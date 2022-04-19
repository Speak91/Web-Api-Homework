using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeWork_1.Controllers
{
    public static class WeathersOperation
    {
        private static List<Weather> _weathersLists = new List<Weather>();
        static Weather weather;
        
        /// <summary>
        ///Добавление погоды 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="temperature"></param>
        public static void AddWeather(DateTime date, int temperature)
        {
            if (Finding(date))
            {
                throw new ArgumentException($"Указанная дата ({date}) уже есть в базе данных");
            }
            else
            {
                weather = new Weather { Date = date, Temperature = temperature };
                _weathersLists.Add(weather);
            }
            
        }

        /// <summary>
        /// Возвращаем список температуры за указанный период
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static IEnumerable<Weather> GetTemperatures(DateTime beginTime, DateTime endTime)
        {
            if (_weathersLists != null || endTime > beginTime)
            {
                foreach (var weather in _weathersLists.ToList())
                {
                    if (weather.Date >= beginTime && weather.Date <= endTime)
                    {
                        yield return weather;
                    }
                }
            }

            else
            {
                yield return null;
            }
        }

        /// <summary>
        /// Удаляем температуру
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        public static void DeleteTemperature(DateTime beginTime, DateTime endTime)
        {
            if (endTime < beginTime)
            {
                throw new Exception("Неверный диапазон дат");
            }

            foreach (var weather in _weathersLists.ToList())
            {
                if (weather.Date >= beginTime && weather.Date <= endTime)
                {
                    _weathersLists.Remove(weather);
                }
            }
        }

        /// <summary>
        /// Обновляем данные
        /// </summary>
        /// <param name="time"></param>
        /// <param name="temperature"></param>
        public static void UpdateTemperature(DateTime time, int temperature)
        {
            if (_weathersLists.Count() == 0)
            {
                throw new ArgumentException("Список пуст");
                
            }

            if (Finding(time))
            {
                throw new ArgumentException("Совпадений не найдено");
            }

            else
            {
                foreach (var weather in _weathersLists.ToList())
                {
                    if (weather.Date == time)
                    {
                        weather.Temperature = temperature;
                    }
                }
            }
           
        }

        /// <summary>
        /// Проверка на наличие даты в списке
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool Finding(DateTime date)
        {
            var match = _weathersLists.Where(x => x.Date == date);
            if (match.Count() == 0)
            {
                return false;
            }

            else
            {
                return true;
            }
        }

       
    }
}
