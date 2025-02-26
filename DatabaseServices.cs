using NPOI.SS.Formula.Functions;

namespace CRUD.Services
{
    public class DatabaseService
    {
        private List<Client> clients;
        private List<Order> orders;
        private List<OrderItem> orderItems;
        private List<Dish> dishes;
        private DatabaseWriter dbWriter;

        public DatabaseService(List<Client> clients, List<Order> orders, List<OrderItem> orderItems, List<Dish> dishes)
        {
            this.clients = clients;
            this.orders = orders;
            this.orderItems = orderItems;
            this.dishes = dishes;
            dbWriter = new DatabaseWriter(); 
        }

        public void ViewClients()
        {
            Console.WriteLine("Клиенты:");
            foreach (var client in clients)
            {
                Console.WriteLine(client);

            }
            Console.ReadKey();
        }

        public void ViewOrders()
        {
            Console.WriteLine("Заказы:");
            foreach (var order in orders)
            {
                Console.WriteLine(order);

            }
            Console.ReadKey();
        }

        public void ViewOrderItems()
        {
            Console.WriteLine("Состав заказов:");
            foreach (var item in orderItems)
            {
                Console.WriteLine(item);

            }
            Console.ReadKey();
        }

        public void ViewDishes()
        {
            Console.WriteLine("Меню:");
            foreach (var dish in dishes)
            {
                Console.WriteLine(dish);

            }
            Console.ReadKey();
        }

        public void DeleteClient(int clientId, string filePath)
        {
            var client = clients.Find(c => c.ClientId == clientId);
            if (client != null)
            {
                clients.Remove(client);
                Console.WriteLine("Клиент удален.");
                var ordersToDelete = orders.FindAll(o => o.ClientId == clientId);
                foreach (var order in ordersToDelete)
                {
                    orders.Remove(order);
                    var orderItemsToDelete = orderItems.FindAll(oi => oi.OrderId == order.OrderId);
                    foreach (var item in orderItemsToDelete)
                    {
                        orderItems.Remove(item);
                    }
                }
                dbWriter.SaveClientsToExcel(filePath, clients);
                dbWriter.SaveOrdersToExcel(filePath, orders);
                dbWriter.SaveOrderItemsToExcel(filePath, orderItems);
            }
            else
            {
                Console.WriteLine("Клиент не найден.");
            }
            Console.ReadKey();
        }

        public void DeleteOrder(int orderId, string filePath)
        {
            var order = orders.Find(o => o.OrderId == orderId);
            if (order != null)
            {
                orders.Remove(order);
                Console.WriteLine("Заказ удален.");

                var orderItemsToDelete = orderItems.FindAll(oi => oi.OrderId == orderId);
                foreach (var item in orderItemsToDelete)
                {
                    orderItems.Remove(item);
                }
                dbWriter.SaveOrdersToExcel(filePath, orders);
                dbWriter.SaveOrderItemsToExcel(filePath, orderItems);
            }
            else
            {
                Console.WriteLine("Заказ не найден.");
            }
            Console.ReadKey();
        }

        public void DeleteOrderItem(int orderItemId, string filePath)
        {
            var orderItem = orderItems.Find(oi => oi.OrderItemId == orderItemId);
            if (orderItem != null)
            {
                orderItems.Remove(orderItem);
                Console.WriteLine("Деталь заказа удалена.");
                dbWriter.SaveOrderItemsToExcel(filePath, orderItems);
            }
            else
            {
                Console.WriteLine("Деталь заказа не найдена.");
            }
            Console.ReadKey();
        }

        public void DeleteDish(int dishId, string filePath)
        {
            var dish = dishes.Find(d => d.DishId == dishId);
            if (dish != null)
            {
                dishes.Remove(dish);
                Console.WriteLine("Блюдо удалено.");

                var orderItemsToDelete = orderItems.FindAll(oi => oi.DishId == dishId);
                foreach (var item in orderItemsToDelete)
                {
                    orderItems.Remove(item);
                }
                dbWriter.SaveDishesToExcel(filePath, dishes);
                dbWriter.SaveOrderItemsToExcel(filePath, orderItems);
            }
            else
            {
                Console.WriteLine("Блюдо не найдено.");
            }
            Console.ReadKey();
        }

        public void EditClient(int clientId, string filePath)
        {
            var client = clients.Find(c => c.ClientId == clientId);
            if (client != null)
            {
                Console.WriteLine("Введите новое имя клиента:");
                client.FirstName = Console.ReadLine();

                Console.WriteLine("Введите новое фамилию клиента:");
                client.LastName = Console.ReadLine();

                Console.WriteLine("Введите новое отчество клиента:");
                client.MiddleName = Console.ReadLine();

                Console.WriteLine("Введите новый адрес клиента:");
                client.Address = Console.ReadLine();

                dbWriter.SaveClientsToExcel(filePath, clients);

                Console.WriteLine("Данные клиента обновлены.");
            }
            else
            {
                Console.WriteLine("Клиент не найден.");
            }
        }

        public void EditOrder(int orderId, string filePath)
        {
            var order = orders.Find(o => o.OrderId == orderId);
            if (order != null)
            {
                Console.WriteLine("Введите новый статус заказа:");
                order.DeliveryStatus = Console.ReadLine();

                Console.WriteLine("Введите новую цену доставки:");
                order.DeliveryPrice = decimal.Parse(Console.ReadLine());

                dbWriter.SaveOrdersToExcel(filePath, orders);

                Console.WriteLine("Данные заказа обновлены.");
            }
            else
            {
                Console.WriteLine("Заказ не найден.");
            }
        }

        public void EditOrderItem(int orderItemId, string filePath)
        {
            var orderItem = orderItems.Find(oi => oi.OrderItemId == orderItemId);
            if (orderItem != null)
            {
                Console.WriteLine("Введите новое количество:");
                orderItem.Quantity = int.Parse(Console.ReadLine());

                dbWriter.SaveOrderItemsToExcel(filePath, orderItems);

                Console.WriteLine("Данные состава заказа обновлены.");
            }
            else
            {
                Console.WriteLine("Деталь заказа не найдена.");
            }
        }

        public void EditDish(int dishId, string filePath)
        {
            var dish = dishes.Find(d => d.DishId == dishId);
            if (dish != null)
            {
                Console.WriteLine("Введите новое название блюда:");
                dish.Name = Console.ReadLine();

                Console.WriteLine("Введите новую цену блюда:");
                dish.Price = decimal.Parse(Console.ReadLine());

                dbWriter.SaveDishesToExcel(filePath, dishes);

                Console.WriteLine("Данные блюда обновлены.");
            }
            else
            {
                Console.WriteLine("Блюдо не найдено.");
            }
        }
        public void AddClientFromConsole(string filePath)
        {
            Console.WriteLine("Введите имя клиента:");
            string firstName = Console.ReadLine();

            Console.WriteLine("Введите фамилию клиента:");
            string lastName = Console.ReadLine();

            Console.WriteLine("Введите отчество клиента:");
            string middleName = Console.ReadLine();

            Console.WriteLine("Введите адрес клиента:");
            string address = Console.ReadLine();

            int newClientId = clients.Count > 0 ? clients.Max(c => c.ClientId) + 1 : 1;

            var newClient = new Client(newClientId, firstName, lastName, middleName, address);

            clients.Add(newClient);
            Console.WriteLine("Новый клиент добавлен.");

            dbWriter.SaveClientsToExcel(filePath, clients);
        }

        public void AddOrderFromConsole(string filePath)
        {
            Console.WriteLine("Введите ID клиента для нового заказа:");
            int clientId = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите статус доставки:");
            string deliveryStatus = Console.ReadLine();

            Console.WriteLine("Введите цену доставки:");
            decimal deliveryPrice = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Введите дату заказа (в формате yyyy-MM-dd):");
            DateTime orderDate = DateTime.Parse(Console.ReadLine());

            int newOrderId = orders.Count > 0 ? orders.Max(o => o.OrderId) + 1 : 1;

            var newOrder = new Order(newOrderId, orderDate, clientId, deliveryPrice, deliveryStatus);

            orders.Add(newOrder);
            Console.WriteLine("Новый заказ добавлен.");

            dbWriter.SaveOrdersToExcel(filePath, orders);
        }


        public void AddOrderItemFromConsole(string filePath)
        {
            Console.WriteLine("Введите ID заказа для добавления элемента:");
            int orderId = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите ID блюда:");
            int dishId = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите количество блюда:");
            int quantity = int.Parse(Console.ReadLine());

            int newOrderItemId = orderItems.Count > 0 ? orderItems.Max(oi => oi.OrderItemId) + 1 : 1;

            var newOrderItem = new OrderItem(newOrderItemId, orderId, dishId, quantity);

            orderItems.Add(newOrderItem);
            Console.WriteLine("Новый элемент в заказ добавлен.");

            dbWriter.SaveOrderItemsToExcel(filePath, orderItems);
        }

        public void AddDishFromConsole(string filePath)
        {
            Console.WriteLine("Введите название блюда:");
            string name = Console.ReadLine();

            Console.WriteLine("Введите цену блюда:");
            decimal price = decimal.Parse(Console.ReadLine());

            int newDishId = dishes.Count > 0 ? dishes.Max(d => d.DishId) + 1 : 1;

            var newDish = new Dish(newDishId, name, price);

            dishes.Add(newDish);
            Console.WriteLine("Новое блюдо добавлено.");

            dbWriter.SaveDishesToExcel(filePath, dishes);
        }

        // Запрос 1: Получить список всех клиентов из Москвы, чьи фамилии начинаются на букву «А»

        public void first_1table()
        {
            var moscowClients = from c in clients
                                where c.Address.Contains("Москва") && c.LastName.StartsWith("А")
                                select new { c.LastName, c.FirstName };

            foreach (var client in moscowClients)
            {
                Console.WriteLine($"{client.LastName} {client.FirstName} ");
            }

            Console.ReadKey();
        }


        // Запрос 2: Получить список всех заказов, где статус доставки «Выполнено»
        // и стоимость доставки меньше 300 в городе Москва
        public void second_2table()
        {
            var ordersWithClients = from o in orders
                                    join client in clients on o.ClientId equals client.ClientId
                                    where o.DeliveryStatus == "Выполнено" && o.DeliveryPrice < 300 && client.Address.Contains("Москва")
                                    select new
                                    {
                                        o.OrderId,
                                        o.DeliveryPrice,
                                        o.DeliveryStatus,
                                        ClientName = client.FirstName + " " + client.LastName,
                                        client.Address
                                    };

            foreach (var order in ordersWithClients)
            {
                Console.WriteLine($"Order ID: {order.OrderId}, Delivery Price: {order.DeliveryPrice}, " +
                                  $"Delivery Status: {order.DeliveryStatus}, Client: {order.ClientName}, Address: {order.Address}");
            }

            Console.ReadKey();
        }

        // Запрос 3: Получить все заказы с элементами,
        // где цена блюда выше 600 рублей, а стоимость доставки выше 400 рублей и количество блюд больше 5
        public void firth_3table()
        {
            var ordersWithDetails = from order in orders
                                    join client in clients on order.ClientId equals client.ClientId
                                    join orderItem in orderItems on order.OrderId equals orderItem.OrderId
                                    join dish in dishes on orderItem.DishId equals dish.DishId
                                    where dish.Price > 600 && order.DeliveryPrice > 400 && orderItem.Quantity > 5
                                    select new
                                    {
                                        order.OrderId,
                                        ClientName = client.FirstName + " " + client.LastName, 
                                        orderItem.Quantity,
                                        dish.Price,
                                        order.DeliveryPrice
                                    };

            // Выводим все заказы
            foreach (var orderDetail in ordersWithDetails)
            {
                Console.WriteLine($"Номер заказа: {orderDetail.OrderId}, Клиент: {orderDetail.ClientName}, " +
                                  $"Количество блюд: {orderDetail.Quantity}, Цена: {orderDetail.Price}, Стоимость доставки: {orderDetail.DeliveryPrice}");
            }

            Console.ReadKey();
        }


        //Запрос 4: Получить общую стоимость заказов на пиццу "Пицца Песто"
        //с учетом доставки за июнь 2020 года для клиентов из Москвы
        public void fourth_3table()
        {
            var pizzaDish = dishes.FirstOrDefault(d => d.Name == "Пицца Песто");
            if (pizzaDish != null)
            {
                var totalCost = (from order in orders
                                 join client in clients on order.ClientId equals client.ClientId
                                 where client.Address.Contains("Москва") && order.OrderDate.Month == 6 && order.OrderDate.Year == 2020
                                 join orderItem in orderItems on order.OrderId equals orderItem.OrderId
                                 where orderItem.DishId == pizzaDish.DishId
                                 select new
                                 {
                                     TotalPrice = (orderItem.Quantity * pizzaDish.Price) + order.DeliveryPrice
                                 }).Sum(x => x.TotalPrice);

                Console.WriteLine($"Общая стоимость заказов на пиццу \"Пицца Песто\" с учетом доставки за июнь 2020 года " +
                    $"для клиентов из Москвы: {totalCost}");
            }
            else
            {
                Console.WriteLine("Пицца \"Пицца Песто\" не найдена.");
            }

            Console.ReadKey();
        }



    }
}
