using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web.Http;

namespace Movit.Sys.Api.Controllers
{
    public class TestController : BaseApiControl
    {

        [Route("~/api/test/{id}/")]

        public async Task<Resp<List<Student>>> GetModel(string id)
        {
            List<Student> result = new List<Student>();
            try
            {
                await Task.Run(() =>
                {
                    result = new List<Student>()
                    {
                        new Student(){
                        age = 1,
                        createtime = DateTime.Now,
                         id=id
                        },
                        new Student(){
                        age = 12,
                        createtime = DateTime.Now,
                         id=id
                        },
                    };
                });
                return Resp.Success(result);
            }
            catch (Exception ex)
            {
                return Resp.BusinessError<List<Student>>(ex.Message, result);
            }
        }

        [Route("~/api/SaveModel")]

        public async Task<Resp<Student>> PostModel(Student model)
        {
            Student result = new Student();
            try
            {
                await Task.Run(() =>
             {
                 result.id = model.id;
             });
                return Resp.Success(result);
            }
            catch (Exception ex)
            {
                return Resp.BusinessError<Student>(ex.Message, result);
            }
        }

        [Route("~/api/List/")]

        public async Task<Resp<Student>> PostList(List<Student> list)
        {
            Student result = new Student();
            try
            {
                await Task.Run(() =>
                {
                    result.id = list[list.Count - 1].id;
                });
                return Resp.Success(result);
            }
            catch (Exception ex)
            {
                return Resp.BusinessError<Student>(ex.Message, result);
            }
        }
    }

    #region 演示类
    [DataContract]
    public class Student
    {
        [Required(ErrorMessage = "付款单编码不能为空!")]
        [StringLength(20, ErrorMessage = "付款单编码长度过长")]
        [DataMember(Order = 1)]
        public string id { get; set; }
        public int age { get; set; }

        [DataMember(Order = 0)]
        public DateTime createtime { get; set; }
    }
    #endregion
}
