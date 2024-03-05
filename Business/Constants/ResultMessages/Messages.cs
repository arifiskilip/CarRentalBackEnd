using Entities.Concrete;

namespace Business.Constants.ResultMessages
{
	public static class Messages
	{
		public static class General
		{
			public static string SuccessAdded = "Ekleme işlemi başarılı.";
			public static string ErrorAdded = "Ekleme işlemi başarısız.";
			public static string SuccessUpdate = "Güncelleme işlemi başarılı.";
			public static string ErrorUpdate = "Güncelleme işlemi başarısız.";
			public static string SuccessDelete = "Silme işlemi başarılı";
			public static string ErrorDelete = "Silme işlemi başarısız.";
			public static string SuccessfulListing = "Listeleme işlemi başarılı.";
			public static string FailedListing = "Listeleme işlemi başarısız.";
		}

		public static class Auth
		{
			public static string SuccessRegister = "Kayıt işlemi başarılı.";
			public static string UserNotFound = "Kullanıcı bulunamadı.";
			public static string UsernameOrPaswordNotFound = "Kullanıcı adı veya şifre hatalı.";
			public static string SuccessLogin = "Giriş işlemi başarılı.";
			public static string CurrentUser = "Böyle bir kullanıcı zaten mevcut.";
			public static string CreatedToken = "Token oluşturuldu.";
		}

		public static class CarImage
		{
			public static string CarImageNotFound = "İlgili resim bulunamadı.";
			public static string MustHaveFiveCarPictrues = "Araç resmi en fazla 5 adet olmalı.";
		}

		public static class Car
		{
			public static string CarNotFound = "İlgili araç bulunamadı.";
		}

		public static class CreditCard
		{
			public static string InvalidCard = "Kartınız geçersizdir lütfen tekrar deneyiniz.";
			public static string ValidCard = "Geçerli kart.";
		}

		public static class Rental
		{
			public static string SuccessRental = "Kiralama işlemi başarılı.";
			public static string CarBeingUsed = "Bu araç şuanda kullanılmaktadır. Lütfen farklı bir araç seçiniz.";
		}
	}
}
