using NPOI.HSSF.UserModel; 
using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public class DatabaseReader
{
    public List<Client> ReadClients(string filePath)
    {
        var clients = new List<Client>();
        using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            var workbook = new HSSFWorkbook(file); 
            var worksheet = workbook.GetSheet("Клиенты");
            for (int row = 1; row <= worksheet.LastRowNum; row++) 
            {
                var currentRow = worksheet.GetRow(row);
                int clientId = int.Parse(currentRow.GetCell(0).ToString());
                string lastName = currentRow.GetCell(1).ToString();
                string firstName = currentRow.GetCell(2).ToString();
                string middleName = currentRow.GetCell(3).ToString();
                string address = currentRow.GetCell(4).ToString();

                var client = new Client(clientId, lastName, firstName, middleName, address);
                clients.Add(client);
            }
        }
        return clients;
    }



    public List<Order> ReadOrders(string filePath)
    {
        var orders = new List<Order>();
        using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            var workbook = new HSSFWorkbook(file); 
            var worksheet = workbook.GetSheet("Заказы"); 

            for (int row = 1; row <= 1994; row++) 
            {
                var currentRow = worksheet.GetRow(row);
                int orderId = int.Parse(currentRow.GetCell(0).ToString());

                string dateString = currentRow.GetCell(1).ToString().Trim(); 

                DateTime orderDate;
                bool isDateValid = false;

                isDateValid = DateTime.TryParseExact(
                dateString,
                new string[] { "dd.MM.YYYY","d.M.YY","M.d.yy" },
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out orderDate);

                if (!isDateValid)
                {
                    Console.WriteLine($"Невозможно распарсить дату: {dateString}");
                    continue;
                }

                int clientId = int.Parse(currentRow.GetCell(2).ToString());

                var deliveryPriceCell = currentRow.GetCell(3);
                decimal deliveryPrice = 0;

                if (deliveryPriceCell != null)
                {
                    if (deliveryPriceCell.CellType == CellType.Numeric)
                    {
                        deliveryPrice = (decimal)deliveryPriceCell.NumericCellValue;
                    }
                    else if (deliveryPriceCell.CellType == CellType.String)
                    {
                        string priceString = deliveryPriceCell.StringCellValue.Trim();
                        priceString = priceString.Replace(" р.", "").Replace(" ", "").Replace(",", ".");

                        if (!decimal.TryParse(priceString, out deliveryPrice))
                        {
                            Console.WriteLine($"Не удалось преобразовать цену: {priceString}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Цена доставки имеет неподдерживаемый формат.");
                    }
                }

                string deliveryStatus = currentRow.GetCell(4)?.ToString().Trim();

                var order = new Order(orderId, orderDate, clientId, deliveryPrice, deliveryStatus);
                orders.Add(order);
            }
        }
        return orders;
    }

    public List<OrderItem> ReadOrderItems(string filePath)
    {
        var orderItems = new List<OrderItem>();
        using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            var workbook = new HSSFWorkbook(file); 
            var worksheet = workbook.GetSheet("Состав заказов");
            for (int row = 1; row <= worksheet.LastRowNum; row++)
            {
                var currentRow = worksheet.GetRow(row);
                int orderItemId = int.Parse(currentRow.GetCell(0).ToString());
                int orderId = int.Parse(currentRow.GetCell(1).ToString());
                int dishId = int.Parse(currentRow.GetCell(2).ToString());
                int quantity = int.Parse(currentRow.GetCell(3).ToString());

                var orderItem = new OrderItem(orderItemId, orderId, dishId, quantity);
                orderItems.Add(orderItem);
            }
        }
        return orderItems;
    }

    public List<Dish> ReadDishes(string filePath)
    {
        var dishes = new List<Dish>();
        using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            var workbook = new HSSFWorkbook(file);
            var worksheet = workbook.GetSheet("Меню");

            for (int row = 1; row <= worksheet.LastRowNum; row++)
            {
                var currentRow = worksheet.GetRow(row);
                int dishId = int.Parse(currentRow.GetCell(0).ToString());

                string dishName = currentRow.GetCell(1).ToString().Trim();

                var dishPriceCell = currentRow.GetCell(2);
                decimal dishPrice = 0;

                if (dishPriceCell != null)
                {
                    if (dishPriceCell.CellType == CellType.Numeric)
                    {
                        dishPrice = (decimal)dishPriceCell.NumericCellValue;
                    }
                    else if (dishPriceCell.CellType == CellType.String)
                    {
                        string priceString = dishPriceCell.StringCellValue.Trim();
                        priceString = priceString.Replace(" р.", "").Replace(" ", "").Replace(",", ".");
                        if (!decimal.TryParse(priceString, out dishPrice))
                        {
                            Console.WriteLine($"Не удалось преобразовать цену блюда: {priceString}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Цена блюда имеет неподдерживаемый формат.");
                    }
                }

                var dish = new Dish(dishId, dishName, dishPrice);
                dishes.Add(dish);
            }
        }
        return dishes;
    }
}


