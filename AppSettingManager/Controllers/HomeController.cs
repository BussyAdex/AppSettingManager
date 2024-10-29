using AppSettingManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace AppSettingManager.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IConfiguration _config;
		private TwilioSettings _twilioSettings;
		private readonly IOptions<TwilioSettings> _twilioOptions;
		private readonly IOptions<SocialLoginSettings> _socialLoginSettings;

		public HomeController(ILogger<HomeController> logger, IConfiguration config, IOptions<TwilioSettings> twilioOptions, IOptions<SocialLoginSettings> socialLoginSettings)
		{
			_logger = logger;
			_config = config;
			_twilioSettings = new TwilioSettings();
			config.GetSection("Twilio").Bind(_twilioSettings);
			_twilioOptions = twilioOptions;
			_socialLoginSettings = socialLoginSettings;
		}

		public IActionResult Index()
		{
			ViewBag.SendGridKey = _config.GetValue<string>("SendGridKey");

			ViewBag.TwilioAuthToken = _twilioOptions.Value.AuthToken;
			ViewBag.TwilioAccountSid = _twilioOptions.Value.AccountSid;
			ViewBag.TwilioPhoneNumber = _twilioOptions.Value.PhoneNumber;

			//ViewBag.TwilioAuthToken = _config.GetSection("Twilio").GetValue<string>("AuthToken");
			//ViewBag.TwilioAccountSid = _config.GetValue<string>("Twilion:AccountSid");
			//ViewBag.TwilioPhoneNumber = _twilioSettings.PhoneNumber;

			//ViewBag.ThirdLevelSettingValue = _config.GetValue<string>("FirstLevelSetting:SecondLevelSetting:BottomLevelSetting");
			//ViewBag.ThirdLevelSettingValue = _config.GetSection("FirstLevelSetting").GetSection("SecondLevelSetting")
			//	.GetValue<string>("BottomLevelSetting");


			ViewBag.ThirdLevelSettingValue = _config.GetSection("FirstLevelSetting").GetSection("SecondLevelSetting")
				.GetSection("BottomLevelSetting").Value;

			ViewBag.FacebookKey = _socialLoginSettings.Value.FacebookSettings.Key;

			ViewBag.GoogleKey = _socialLoginSettings.Value.GoogleSettings.Key;

			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
