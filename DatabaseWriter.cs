using System.Collections.Generic;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace CRUD.Services
{
    public class DatabaseWriter
    {
        public void SaveClientsToExcel(string filePath, List<Client> clients)
        {
            using (var file = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
            {
                var workbook = new HSSFWorkbook(file);
                var worksheet = workbook.GetSheet("Клиенты");

                for (int i = 1; i <= worksheet.LastRowNum; i++)
                {
                    var row = worksheet.GetRow(i);
                    if (row != null)
                    {
                        worksheet.RemoveRow(row); 
                    }
                }


                for (int i = 0; i < clients.Count; i++)
                {
                    var row = worksheet.CreateRow(i + 1); 
                    row.CreateCell(0).SetCellValue(clients[i].ClientId);
                    row.CreateCell(1).SetCellValue(clients[i].LastName);
                    row.CreateCell(2).SetCellValue(clients[i].FirstName);
                    row.CreateCell(3).SetCellValue(clients[i].MiddleName);
                    row.CreateCell(4).SetCellValue(clients[i].Address);
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    workbook.Write(fileStream);
                }
            }
        }

        public void SaveOrdersToExcel(string filePath, List<Order> orders)
        {
            using (var file = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
            {
                var workbook = new HSSFWorkbook(file);
                var worksheet = workbook.GetSheet("Заказы");

                for (int i = 1; i <= worksheet.LastRowNum; i++)
                {
                    var row = worksheet.GetRow(i);
                    if (row != null)
                    {
                        worksheet.RemoveRow(row); 
                    }
                }

                for (int i = 0; i < orders.Count; i++)
                {
                    var row = worksheet.CreateRow(i + 1); 
                    row.CreateCell(0).SetCellValue(orders[i].OrderId);
                    row.CreateCell(1).SetCellValue(orders[i].ClientId);
                    row.CreateCell(2).SetCellValue(orders[i].OrderDate.ToString("dd.MM.yyyy"));
                    row.CreateCell(3).SetCellValue(orders[i].DeliveryPrice.ToString());
                    row.CreateCell(4).SetCellValue(orders[i].DeliveryStatus);
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    workbook.Write(fileStream);
                }
            }
        }

        public void SaveOrderItemsToExcel(string filePath, List<OrderItem> orderItems)
        {
            using (var file = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
            {
                var workbook = new HSSFWorkbook(file);
                var worksheet = workbook.GetSheet("Состав заказов");

                for (int i = 1; i <= worksheet.LastRowNum; i++)
                {
                    var row = worksheet.GetRow(i);
                    if (row != null)
                    {
                        worksheet.RemoveRow(row); 
                    }
                }

                for (int i = 0; i < orderItems.Count; i++)
                {
                    var row = worksheet.CreateRow(i + 1); 
                    row.CreateCell(0).SetCellValue(orderItems[i].OrderItemId);
                    row.CreateCell(1).SetCellValue(orderItems[i].OrderId);
                    row.CreateCell(2).SetCellValue(orderItems[i].DishId);
                    row.CreateCell(3).SetCellValue(orderItems[i].Quantity);
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    workbook.Write(fileStream);
                }
            }
        }

        public void SaveDishesToExcel(string filePath, List<Dish> dishes)
        {
            using (var file = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
            {
                var workbook = new HSSFWorkbook(file);
                var worksheet = workbook.GetSheet("Меню");

                for (int i = 1; i <= worksheet.LastRowNum; i++)
                {
                    var row = worksheet.GetRow(i);
                    if (row != null)
                    {
                        worksheet.RemoveRow(row);
                    }
                }

                for (int i = 0; i < dishes.Count; i++)
                {
                    var row = worksheet.CreateRow(i + 1);
                    row.CreateCell(0).SetCellValue(dishes[i].DishId);
                    row.CreateCell(1).SetCellValue(dishes[i].Name);
                    row.CreateCell(2).SetCellValue(dishes[i].Price.ToString());
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    workbook.Write(fileStream);
                }
            }
        }
    }
}
