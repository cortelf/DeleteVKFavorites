using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Enums.SafetyEnums;
using VkNet.Exception;
using VkNet.Utils;
using RuCaptcha;
using VkNet.Model;

namespace DeleteVKFavorites
{
    internal class Program
    {
        static private string Login { get; set; }
        static private string Password { get; set; }
        static private readonly ulong AppId = 6469990;
        static private VkApi Api { get; } = new VkApi();
        static private string CaptchaString { get; set; } = null;
        static private long? Seed { get; set; } = null;
        static private RuCaptcha.RuCaptcha Captcha { get; set; }


        static void Main(string[] args)
        {
            #region Init

            Console.Write("Введите логин: ");
            Login = Console.ReadLine();

            Console.Write("Введите пароль: ");
            Password = Console.ReadLine();

            Console.Write("Введите api ключ RuCaptcha: ");
            Captcha = new RuCaptcha.RuCaptcha(Console.ReadLine());

            #endregion

            try
            {
                Api.Authorize(new ApiAuthParams
                {
                    ApplicationId = AppId,
                    Login = Login,
                    Password = Password,
                    Settings = Settings.All

                });
                string token = Api.Token;
                DeletePhotos();
                DeletePosts();
            }  catch (CaptchaException ce)
            {
                Console.WriteLine("Неверный api RuCaptcha!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Неверный логин или пароль");
            }
            Console.Write("Нажмите любую клавишу для выхода... ");
            Console.Read();
        }

        static void DeletePhotos()
        {

            dynamic photos = Api.Fave.GetPhotos();

            Deleter(photos, 0);
            
        }

        static void Deleter(dynamic objects, byte mode)
        {
            var noDelete = new List<long>(); // то, что удалить нельзя
            while (objects.Count != 0)
            {
                foreach (var obj in objects)
                {
                    long realId = obj.Id ?? 0;

                    try
                    {
                        if (realId != 0)
                        {
                            Api.Likes.Delete(mode == 0 ? LikeObjectType.Photo : LikeObjectType.Post, realId, obj.OwnerId, Seed, CaptchaString);
                            Thread.Sleep(350); // чтобы не словить Слишком много запросов
                        }
                    }
                    catch (CaptchaNeededException exc)
                    {
                        try
                        {
                            long serverCaptchaId = Captcha.SendCaptcha(exc.Img);
                            Thread.Sleep(5500); // получение результата
                            CaptchaString = Captcha.GetCaptcha(serverCaptchaId);
                            Seed = exc.Sid;
                        }
                        catch
                        {

                            throw new CaptchaException();
                        }
                    }
                    catch
                    {
                        noDelete.Add(realId);
                    }
                }
                if (mode == 0)
                {
                    objects = Api.Fave.GetPhotos(offset: noDelete.Count);
                }
                else
                {
                    objects = Api.Fave.GetPosts(offset: noDelete.Count).WallPosts;
                }
            }
        }
        static void DeletePosts()
        {

            dynamic posts = Api.Fave.GetPosts().WallPosts;

            Deleter(posts, 1);
        }
    }
}
