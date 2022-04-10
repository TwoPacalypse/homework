using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace generic
{
    internal class MyThread
    {
        static void Main(string[] args)
        {
            DummyRequestHandler handler = new DummyRequestHandler();

            Console.WriteLine("Приложение запущено.\nВведите текст запроса для отправки, для выхода - /q");
            var command = Console.ReadLine();
            var args_ = new List<string>();


            while (command != "/q")
            {
                args_.Clear();
                Console.WriteLine("Введите аргумент сообщения, если аргументы не нужны - /end");
                var message = Console.ReadLine();
                while (message != "/end")
                {
                    args_.Add(message);
                    Console.WriteLine("Введите аргумент сообщения, если аргументы не нужны - /end");
                    message = Console.ReadLine();
                }

                args_.Add(Guid.NewGuid().ToString("D"));
                Console.WriteLine("Будет отправлено сообщение: \""+command+ "\". Идентификатор - "+ args_.Last());
                Thread myNewThread = new Thread(() => handler.HandleRequest(command, args_.ToArray()));
                myNewThread.IsBackground = true;
                myNewThread.Start();

                Console.WriteLine("\nНовый запрос.\nВведите текст запроса для отправки, для выхода - /q");
                command = Console.ReadLine();
            }
            Console.WriteLine("Угадал!");
        }
    }

    public interface IRequestHandler
    {
        /// <summary>
        /// Обработать запрос.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="arguments">Аргументы запроса.</param>
        /// <returns>Результат выполнения запроса.</returns>
        string HandleRequest(string message, string[] arguments);
    }

    /// <summary>
    /// Тестовый обработчик запросов.
    /// </summary>
    public class DummyRequestHandler : IRequestHandler
    {
        /// <inheritdoc />
        public string HandleRequest(string message, string[] arguments)
        {
            try
            {
                // Притворяемся, что делаем что то.
                Thread.Sleep(5000);
                if (message.Contains("упади"))
                {
                    throw new Exception("Я упал, как сам просил");
                }
                Console.WriteLine("Сообщение с идентификатором " + arguments.Last() + " получило ответ -  " + Guid.NewGuid().ToString("D"));
            }
            catch(Exception ex)
            {
                Console.WriteLine("Сообщение с идентификатором " + arguments.Last() + " упало - " + ex.Message);
            }
            return Guid.NewGuid().ToString("D");
        }
    }
}