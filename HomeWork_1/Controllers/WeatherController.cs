using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeWork_1.Controllers
{
    [ApiController]
    [Route("/")]
    public class WeatherController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        /// <summary>
        /// Сохраняем температуру в указанную дату
        /// </summary>
        /// <param name="date"></param>
        /// <param name="temperature"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public IActionResult Add( DateTime date, int temperature)
        {
            WeathersOperation.AddWeather(date, temperature);
            return Ok();
        }

        /// <summary>
        /// Вывод всей температуры за указанный промежуток времени
        /// </summary>
        /// <returns></returns>
        [HttpGet("readall")]
        public IEnumerable<Weather> GetTemperatures([FromQuery] DateTime beginTime, [FromQuery] DateTime endTime)
        {
            return WeathersOperation.GetTemperatures(beginTime, endTime);
        }

        /// <summary>
        /// Удаление температуры за указанный промежуток времени
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] DateTime beginTime, [FromQuery] DateTime endTime)
        {
            WeathersOperation.DeleteTemperature(beginTime, endTime);
            return Ok();
        }

        /// <summary>
        /// Отредактировать показатель температуры в указанное время
        /// </summary>
        /// <param name="date"></param>
        /// <param name="temperature"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public ActionResult Update([FromQuery] DateTime date, [FromQuery] int temperature)
        {
            try
            {
                WeathersOperation.UpdateTemperature(date, temperature);
                return Ok();
            }

            catch (Exception)
            {

                return BadRequest();
            }          
        }
    }
}
