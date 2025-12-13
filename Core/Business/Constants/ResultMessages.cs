using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Business.Constants;
public static class ResultMessages
{
    public static string SuccessCreated = "Başarıyla eklendi.";
    public static string SuccessUpdated = "Başarıyla güncellendi.";
    public static string SuccessDeleted = "Başarıyla silindi.";
    public static string SuccessListed = "Başarıyla listelendi.";
    public static string SuccessGet = "Başarıyla getirildi.";

    public static string ErrorCreated = "Ekleme işlemi başarısız.";
    public static string ErrorUpdated = "Güncelleme işlemi başarısız.";
    public static string ErrorDeleted = "Silme işlemi başarısız.";
    public static string ErrorListed = "Listeleme işlemi başarısız.";
    public static string ErrorGet = "Getirme işlemi başarısız.";

    public static string NotFound = "Aradığınız veri kayıtlarda mevcut değil...";
    public static string InvalidId = "Geçersiz id değeri...";
    public static string AlreadyDeleted = "Kayıt zaten silinmiş...";

    public static string ErrorFormData = "Formdan gelen veri eksik ya da hatalı...";

    public static string IsTrue = "Evet";
    public static string IsFalse = "Hayır";
}
