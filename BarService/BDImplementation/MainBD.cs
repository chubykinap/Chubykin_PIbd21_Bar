using BarModel;
using BarService.BindingModels;
using BarService.Interfaces;
using BarService.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Data.Entity;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace BarService.BDImplementation
{
    public class MainBD : IMainService
    {
        private BarDBContext context;

        public MainBD(BarDBContext context)
        {
            this.context = context;
        }

        public List<OrderViewModel> GetList()
        {
            List<OrderViewModel> result = context.Orders
                .Select(rec => new OrderViewModel
                {
                    ID = rec.ID,
                    CustomerID = rec.CustomerID,
                    CocktailID = rec.CocktailID,
                    ExecutorID = rec.ExecutorID,
                    DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
                                SqlFunctions.DateName("mm", rec.DateCreate) + " " +
                                SqlFunctions.DateName("yyyy", rec.DateCreate),
                    DateImplement = rec.DateImplement == null ? "" :
                                        SqlFunctions.DateName("dd", rec.DateImplement.Value) + " " +
                                        SqlFunctions.DateName("mm", rec.DateImplement.Value) + " " +
                                        SqlFunctions.DateName("yyyy", rec.DateImplement.Value),
                    Status = rec.Status.ToString(),
                    Count = rec.Count,
                    Sum = rec.Sum,
                    CustomerFIO = rec.Customer.CustomerFIO,
                    CocktailName = rec.Cocktail.CocktailName,
                    ExecutorName = rec.Executor.ExecutorFIO
                })
                .ToList();
            return result;
        }

        public void CreateOrder(OrderBindModel model)
        {
            var order = new Order
            {
                CustomerID = model.CustomerID,
                CocktailID = model.CocktailID,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = OrderStatus.Принят
            };
            context.Orders.Add(order);
            context.SaveChanges();
            var client = context.Customers.FirstOrDefault(x => x.ID == model.CustomerID);
            SendEmail(client.Mail, "Оповещение по заказам", string.Format("Заказ №{0} от {1} создан успешно", order.ID,
            order.DateCreate.ToShortDateString()));
        }

        public void TakeOrderInWork(OrderBindModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Order element = context.Orders.Include(rec => rec.Customer).FirstOrDefault(rec => rec.ID == model.ID);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    var CocktailElements = context.ElementRequirements
                                                .Include(rec => rec.Element)
                                                .Where(rec => rec.CocktailID == element.CocktailID);
                    foreach (var CocktailElement in CocktailElements)
                    {
                        int countOnStorages = CocktailElement.Count * element.Count;
                        var StorageElements = context.ElementRequirements
                                                    .Where(rec => rec.ElementID == CocktailElement.ElementID);
                        foreach (var StorageElement in StorageElements)
                        {
                            if (StorageElement.Count >= countOnStorages)
                            {
                                StorageElement.Count -= countOnStorages;
                                countOnStorages = 0;
                                context.SaveChanges();
                                break;
                            }
                            else
                            {
                                countOnStorages -= StorageElement.Count;
                                StorageElement.Count = 0;
                                context.SaveChanges();
                            }
                        }
                        if (countOnStorages > 0)
                        {
                            throw new Exception("Не достаточно компонента " +
                                CocktailElement.Element.ElementName + " требуется " +
                                CocktailElement.Count + ", не хватает " + countOnStorages);
                        }
                    }
                    element.ExecutorID = model.ExecutorID;
                    element.DateImplement = DateTime.Now;
                    element.Status = OrderStatus.Выполняется;
                    context.SaveChanges();
                    SendEmail(element.Customer.Mail, "Оповещение по заказам", string.Format("Заказ №{0} от {1} передеан в работу", element.ID, element.DateCreate.ToShortDateString()));
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void FinishOrder(int ID)
        {
            Order element = context.Orders.Include(rec => rec.Customer).FirstOrDefault(rec => rec.ID == ID);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = OrderStatus.Готов;
            context.SaveChanges();
            SendEmail(element.Customer.Mail, "Оповещение по заказам", string.Format("Заказ №{0} от {1} передан на оплату", element.ID, element.DateCreate.ToShortDateString()));
        }

        public void PayOrder(int ID)
        {
            Order element = context.Orders.Include(rec => rec.Customer).FirstOrDefault(rec => rec.ID == ID);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = OrderStatus.Оплачен;
            context.SaveChanges();
            SendEmail(element.Customer.Mail, "Оповещение по заказам", string.Format("Заказ №{0} от {1} оплачен успешно", element.ID, element.DateCreate.ToShortDateString()));
        }

        public void PutElementOnStorage(ElementStorageBindModel model)
        {
            ElementStorage element = context.ElementStorages
                                                .FirstOrDefault(rec => rec.StorageID == model.StorageID &&
                                                                    rec.ElementID == model.ElementID);
            if (element != null)
            {
                element.Count = model.Count;
            }
            else
            {
                context.ElementStorages.Add(new ElementStorage
                {
                    StorageID = model.StorageID,
                    ElementID = model.ElementID,
                    Count = model.Count
                });
            }
            context.SaveChanges();
        }

        private void SendEmail(string mailAddress, string subject, string text)
        {
            MailMessage objMailMessage = new MailMessage();
            SmtpClient objSmtpClient = null;
            try
            {
                objMailMessage.From = new MailAddress(ConfigurationManager.AppSettings["MailLogin"]);
                objMailMessage.To.Add(new MailAddress(mailAddress));
                objMailMessage.Subject = subject;
                objMailMessage.Body = text;
                objMailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                objMailMessage.BodyEncoding = System.Text.Encoding.UTF8;

                objSmtpClient = new SmtpClient("smtp.gmail.com", 587);
                objSmtpClient.UseDefaultCredentials = false;
                objSmtpClient.EnableSsl = true;
                objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                objSmtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailLogin"],
                    ConfigurationManager.AppSettings["MailPassword"]);
                objSmtpClient.Send(objMailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objMailMessage = null;
                objSmtpClient = null;
            }
        }
    }
}
