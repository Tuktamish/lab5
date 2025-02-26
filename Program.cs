using CRUD.Services;
using System;
using System.Collections.Generic;
using System.IO;

public class Logger
{
    private string logFilePath;

    public Logger(string logFilePath)
    {
        this.logFilePath = logFilePath;
    }

    public void Log(string message)
    {
        // Добавление записи в файл с меткой времени
        using (StreamWriter writer = new StreamWriter(logFilePath, true))
        {
            writer.WriteLine($"{DateTime.Now}: {message}");
        }
    }
}

public class Program
{
    private static List<Client> clients = new List<Client>();
    private static List<Order> orders = new List<Order>();
    private static List<OrderItem> orderItems = new List<OrderItem>();
    private static List<Dish> dishes = new List<Dish>();
    private static Logger logger;

    public static void Main(string[] args)
    {
        // Запрос у пользователя на создание нового файла или использование существующего
        Console.WriteLine("Хотите создать новый лог-файл или использовать существующий?");
        Console.WriteLine("1. Новый файл");
        Console.WriteLine("2. Использовать существующий");
        string choice = Console.ReadLine();

        string logFilePath;

        if (choice == "1")
        {
            // Если выбран новый файл
            Console.WriteLine("Введите имя нового файла лога (например, 'log.txt'):");
            logFilePath = Console.ReadLine();

            // Создаем новый файл лога
            using (StreamWriter writer = new StreamWriter(logFilePath))
            {
                writer.WriteLine("Новый лог-файл создан: " + DateTime.Now);
            }
        }
        else if (choice == "2")
        {
            // Если выбран существующий файл
            Console.WriteLine("Введите имя существующего файла лога (например, 'log.txt'):");
            logFilePath = Console.ReadLine();

            // Проверяем, существует ли файл
            if (!File.Exists(logFilePath))
            {
                Console.WriteLine("Файл не найден. Завершаем программу.");
                return;
            }
        }
        else
        {
            Console.WriteLine("Неверный выбор.");
            return;
        }

        // Создаем логгер с выбранным файлом
        logger = new Logger(logFilePath);

        var dbReader = new DatabaseReader();
        string filePath = "LR5-var8.xls";

        clients = dbReader.ReadClients(filePath);
        orders = dbReader.ReadOrders(filePath);
        orderItems = dbReader.ReadOrderItems(filePath);
        dishes = dbReader.ReadDishes(filePath);

        var dbService = new DatabaseService(clients, orders, orderItems, dishes);

        // Логирование начала сессии
        logger.Log("Сессия началась.");

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Просмотр отдельных листов базы данных");
            Console.WriteLine("2. Удаление элемента");
            Console.WriteLine("3. Корректировка элемента");
            Console.WriteLine("4. Добавить новый элемент");
            Console.WriteLine("5. Запросы");
            Console.WriteLine("6. Выход");

            var userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("Выберите лист:");
                    Console.WriteLine("1. Клиенты");
                    Console.WriteLine("2. Заказы");
                    Console.WriteLine("3. Состав заказов");
                    Console.WriteLine("4. Меню");
                    var viewChoice = Console.ReadLine();
                    switch (viewChoice)
                    {
                        case "1":
                            dbService.ViewClients();
                            logger.Log("Просмотрены клиенты.");
                            break;
                        case "2":
                            dbService.ViewOrders();
                            logger.Log("Просмотрены заказы.");
                            break;
                        case "3":
                            dbService.ViewOrderItems();
                            logger.Log("Просмотрены составы заказов.");
                            break;
                        case "4":
                            dbService.ViewDishes();
                            logger.Log("Просмотрены блюда.");
                            break;
                        default:
                            Console.WriteLine("Неверный выбор.");
                            break;
                    }
                    break;

                case "2":
                    Console.Clear();
                    Console.WriteLine("Выберите лист для удаления элемента:");
                    Console.WriteLine("1. Клиенты");
                    Console.WriteLine("2. Заказы");
                    Console.WriteLine("3. Состав заказов");
                    Console.WriteLine("4. Меню");
                    var deleteChoice = Console.ReadLine();
                    switch (deleteChoice)
                    {
                        case "1":
                            Console.WriteLine("Введите ID клиента для удаления:");
                            var clientId = int.Parse(Console.ReadLine());
                            dbService.DeleteClient(clientId, filePath);
                            logger.Log($"Клиент с ID {clientId} удалён.");
                            break;
                        case "2":
                            Console.WriteLine("Введите ID заказа для удаления:");
                            var orderId = int.Parse(Console.ReadLine());
                            dbService.DeleteOrder(orderId, filePath);
                            logger.Log($"Заказ с ID {orderId} удалён.");
                            break;
                        case "3":
                            Console.WriteLine("Введите ID элемента для удаления:");
                            var itemId = int.Parse(Console.ReadLine());
                            dbService.DeleteOrderItem(itemId, filePath);
                            logger.Log($"Элемент с ID {itemId} удалён из состава заказа.");
                            break;
                        case "4":
                            Console.WriteLine("Введите ID блюда для удаления:");
                            var dishId = int.Parse(Console.ReadLine());
                            dbService.DeleteDish(dishId, filePath);
                            logger.Log($"Блюдо с ID {dishId} удалено.");
                            break;
                        default:
                            Console.WriteLine("Неверный выбор.");
                            break;
                    }
                    break;

                case "3":
                    Console.Clear();
                    Console.WriteLine("Выберите лист для редактирования элемента:");
                    Console.WriteLine("1. Клиенты");
                    Console.WriteLine("2. Заказы");
                    Console.WriteLine("3. Состав заказов");
                    Console.WriteLine("4. Меню");
                    var editChoice = Console.ReadLine();
                    switch (editChoice)
                    {
                        case "1":
                            Console.WriteLine("Введите ID клиента для редактирования:");
                            var clientId = int.Parse(Console.ReadLine());
                            dbService.EditClient(clientId, filePath);
                            logger.Log($"Данные клиента с ID {clientId} были обновлены.");
                            break;
                        case "2":
                            Console.WriteLine("Введите ID заказа для редактирования:");
                            var orderId = int.Parse(Console.ReadLine());
                            dbService.EditOrder(orderId, filePath);
                            logger.Log($"Данные заказа с ID {orderId} были обновлены.");
                            break;
                        case "3":
                            Console.WriteLine("Введите ID элемента для редактирования:");
                            var itemId = int.Parse(Console.ReadLine());
                            dbService.EditOrderItem(itemId, filePath);
                            logger.Log($"Данные элемента с ID {itemId} в составе заказа были обновлены.");
                            break;
                        case "4":
                            Console.WriteLine("Введите ID блюда для редактирования:");
                            var dishId = int.Parse(Console.ReadLine());
                            dbService.EditDish(dishId, filePath);
                            logger.Log($"Данные блюда с ID {dishId} были обновлены.");
                            break;
                        default:
                            Console.WriteLine("Неверный выбор.");
                            break;
                    }
                    break;

                case "4":
                    Console.Clear();
                    Console.WriteLine("Выберите лист для добавления элемента:");
                    Console.WriteLine("1. Клиенты");
                    Console.WriteLine("2. Заказы");
                    Console.WriteLine("3. Состав заказов");
                    Console.WriteLine("4. Меню");
                    var addChoice = Console.ReadLine();
                    switch (addChoice)
                    {
                        case "1":
                            dbService.AddClientFromConsole(filePath);
                            logger.Log("Добавлен новый клиент.");
                            break;
                        case "2":
                            dbService.AddOrderFromConsole(filePath);
                            logger.Log("Добавлен новый заказ.");
                            break;
                        case "3":
                            dbService.AddOrderItemFromConsole(filePath);
                            logger.Log("Добавлен новый элемент в заказ.");
                            break;
                        case "4":
                            dbService.AddDishFromConsole(filePath);
                            logger.Log("Добавлено новое блюдо.");
                            break;
                        default:
                            Console.WriteLine("Неверный выбор.");
                            break;
                    }
                    break;

                case "5":
                    Console.Clear();
                    Console.WriteLine("Запрос 1: Получить список всех клиентов из Москвы, чьи фамилии начинаются на букву «А»");
                    Console.WriteLine("Запрос 2: Получить список всех заказов, где статус доставки «Выполнено» и " +
                        "стоимость доставки меньше 300 в городе Москва");
                    Console.WriteLine("Запрос 3: Получить все заказы с элементами, где цена блюда выше 600 рублей, " +
                        "а стоимость доставки выше 400 рублей и количество блюд больше 5");
                    Console.WriteLine("Запрос 4: Получить общую стоимость заказов на пиццу \"Песто\" " +
                        "с учетом доставки за июль 2020 года для клиентов из Москвы");
                    var queryChoice = Console.ReadLine();
                    switch (queryChoice)
                    {
                        case "1":
                            dbService.first_1table();
                            logger.Log("Запрос 1 выполнен.");
                            break;
                        case "2":
                            dbService.second_2table();
                            logger.Log("Запрос 2 выполнен.");
                            break;
                        case "3":
                            dbService.firth_3table();
                            logger.Log("Запрос 3 выполнен.");
                            break;
                        case "4":
                            dbService.fourth_3table();
                            logger.Log("Запрос 4 выполнен.");
                            break;
                        default:
                            Console.WriteLine("Неверный выбор.");
                            break;
                    }
                    break;

                case "6":
                    logger.Log("Сессия завершена.");
                    return;

                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }
    }
}
