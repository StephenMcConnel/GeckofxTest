using System;
using System.IO;
using System.Windows.Forms;
using Gecko;

namespace GeckofxTest
{
    class MainClass
    {
        [STAThread]
        public static void Main(string[] args)
        {
			string folder = GetRuntimeFolder();
			var testDoc = String.Format("file://{0}", Path.Combine(folder, "Recordmp3js/index.html"));
			if (args.Length > 0 && !String.IsNullOrEmpty(args[0]) && (args[0].StartsWith("http") || args[0].StartsWith("file://")))
				testDoc = args[0];

			var xulRunnerPath = Path.Combine(folder, "Firefox");
			Xpcom.Initialize(xulRunnerPath);

            var gwb = new GeckoWebBrowser();
            gwb.Dock = DockStyle.Fill;

			var config = new GeckoWebBrowser();
            config.Dock = DockStyle.Fill;

			var splitter = new SplitContainer();
            splitter.Panel1.Controls.Add(gwb);
            splitter.Panel2.Controls.Add(config);
            splitter.Dock = DockStyle.Fill;
 
            var f = new Form();
            f.Width = 1200;
			f.Height = 600;
            f.Controls.Add(splitter);
            config.Navigate("about:config");
			gwb.Navigate(testDoc);

            Application.Run(f);
        }

		public static string GetRuntimeFolder()
		{
			var asm = System.Reflection.Assembly.GetExecutingAssembly();
			var file = asm.CodeBase.Replace("file://", String.Empty);
			if (Environment.OSVersion.Platform == PlatformID.Win32NT)
				file = file.TrimStart('/');
			return Path.GetDirectoryName(file);
		}
    }
}
