using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;


//Alexander Francis Gusmano

namespace AutoPrintP1
{
    class Program
    {

        static void Main(string[] args)
        {
            

            var sys = (System.Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE"));//Gives the Architecture of OS
            var os = (System.Environment.OSVersion.Version);//OS Version
            var home = (AppDomain.CurrentDomain.BaseDirectory);

            //ProcessStartInfo pCmd1 = new ProcessStartInfo();
            Process pDism1 = new Process();
            Process pCmd1 = new Process();
            Process pNet1 = new Process();
            Process pNet2 = new Process();
            Process pDll1 = new Process();
            Process pMsi1 = new Process();
            Process pReg1 = new Process();
            Process pReg2 = new Process();
            Process pMomClient = new Process();

            
            var pInstallx64 = ""+home+"PrinterInstall\\x64\\Driver\\cns30ma64.inf";  //64bit driver C:\\Users\\Alexander\\Desktop\\PrinterInstall\\x64\\Driver\\cns30ma64.inf
            var pConfigx64BnW = "" + home + "\\PrinterInstall\\x64\\CSP_B&W.reg";//BnW.reg
            var pConfigx64Color = "" + home + "\\PrinterInstall\\x64\\CSP_Color.reg";//Color.reg
            var pServer = "acaduniflow.nyit.edu";
            var pName = "Canon_Secure_Print";
            //When importing registry key it does import the settings for the printer
            //Printer Properties have to be manually changed
            //Black & White/ Color, Staple, # of Drawers 4
            var pInstallx32 = "" + home + "PrinterInstall\\x64\\Driver\\cns30m.inf";   //32bit driver
            var pErrorCode = "";
            var pTask = "";

            string[] lines = {pErrorCode+"|"+pTask};
            System.IO.File.WriteAllLines(""+ home +"Log.txt", lines);

            ////////////////////Determine OS and Architecture////////////////////////
            //if (sys == "AMD64")//64bit
            { 

                {// DISM is a command-line tool. The output is text. Find a way to redirect the output to a text file

                    ///////////////////////Enable LPRPort/////////////////////////////

                    
                    pDism1.StartInfo.FileName = "dism.exe";
                    pDism1.StartInfo.Arguments = "/online /enable-feature /featurename:Printing-Foundation-LPRPortMonitor";
                    //pDism1.StartInfo.ErrorDialog = true;
                    pDism1.StartInfo.UseShellExecute = false;
                    pDism1.StartInfo.RedirectStandardOutput = true;
                    pDism1.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                    pDism1.Start();
                    pDism1.WaitForExit();
                    Console.WriteLine(pDism1.StandardOutput.ReadToEnd());//operation
                    Console.ReadKey();
                    //output stream



                    //////////////////////////Install Printer Driver///////////////////////////
                    
                    
                    pCmd1.StartInfo.FileName = "pnputil.exe";
                    pCmd1.StartInfo.Arguments = "-i -a \"" + pInstallx64 + "\"";
                    pCmd1.StartInfo.CreateNoWindow = true;
                    pCmd1.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                    pCmd1.Start();
                    pCmd1.WaitForExit();
                    
                    

                    //////////////Create LPR Port//////////////////
                    pNet1.StartInfo.FileName = "net.exe";
                    pNet1.StartInfo.Arguments = "stop spooler";
                    pNet1.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                    pNet1.Start();
                    pNet1.WaitForExit();
                   
                    //Create registry key
                    ////////////Add BnWPort////////////
                    Microsoft.Win32.RegistryKey printKeyAddBnW1;
                    //printKeyAddBnW1 = Microsoft.Win32.Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\Print\\Monitors\\LPR Port\\Ports\\acaduniflow.nyit.edu:Canon_Secure_Print");
                    //printKeyAddBnW1.SetValue("Server Name", "acaduniflow.nyit.edu");
                    printKeyAddBnW1 = Microsoft.Win32.Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\Print\\Monitors\\LPR Port\\Ports\\" + pServer + ":"+ pName);
                    printKeyAddBnW1.SetValue("Server Name", pServer);
                    printKeyAddBnW1.SetValue("Printer Name", pName);
                    printKeyAddBnW1.SetValue("OldSunCompatibility",00000000, Microsoft.Win32.RegistryValueKind.DWord);
                    printKeyAddBnW1.SetValue("HpUxCompatibility",00000000, Microsoft.Win32.RegistryValueKind.DWord);
                    printKeyAddBnW1.SetValue("EnableBannerPage",00000000, Microsoft.Win32.RegistryValueKind.DWord);
                    printKeyAddBnW1.Close();

                    Microsoft.Win32.RegistryKey printKeyAddBnW2;
                    //printKeyAddBnW2 = Microsoft.Win32.Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\Print\\Monitors\\LPR Port\\Ports\\acaduniflow.nyit.edu:Canon_Secure_Print\\Timeouts");
                    printKeyAddBnW2 = Microsoft.Win32.Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\Print\\Monitors\\LPR Port\\Ports\\" + pServer + ":" + pName + "\\Timeouts");
                    printKeyAddBnW2.SetValue("CommandTimeout",120, Microsoft.Win32.RegistryValueKind.DWord);
                    printKeyAddBnW2.SetValue("DataTimeout",300, Microsoft.Win32.RegistryValueKind.DWord);
                    printKeyAddBnW2.Close();
                    
                    ///////////Add ColorPort////////////
                    Microsoft.Win32.RegistryKey printKeyAddColor1;
                    printKeyAddColor1 = Microsoft.Win32.Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\Print\\Monitors\\LPR Port\\Ports\\acaduniflow.nyit.edu:Canon_Secure_Print_Color");
                    printKeyAddColor1.SetValue("Server Name", "acaduniflow.nyit.edu");
                    printKeyAddColor1.SetValue("Printer Name", "Canon_Secure_Print_Color");
                    printKeyAddColor1.SetValue("OldSunCompatibility", 00000000, Microsoft.Win32.RegistryValueKind.DWord);
                    printKeyAddColor1.SetValue("HpUxCompatibility", 00000000, Microsoft.Win32.RegistryValueKind.DWord);
                    printKeyAddColor1.SetValue("EnableBannerPage", 00000000, Microsoft.Win32.RegistryValueKind.DWord);
                    printKeyAddColor1.Close();

                    Microsoft.Win32.RegistryKey printKeyAddColor2;
                    printKeyAddColor2 = Microsoft.Win32.Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\Print\\Monitors\\LPR Port\\Ports\\acaduniflow.nyit.edu:Canon_Secure_Print_Color\\Timeouts");
                    printKeyAddColor2.SetValue("CommandTimeout", 120, Microsoft.Win32.RegistryValueKind.DWord);
                    printKeyAddColor2.SetValue("DataTimeout", 300, Microsoft.Win32.RegistryValueKind.DWord);
                    printKeyAddColor2.Close();

                    

                    pNet2.StartInfo.FileName = "net.exe";
                    pNet2.StartInfo.Arguments = "start spooler";
                    pNet2.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                    pNet2.Start();
                    pNet2.WaitForExit();



                    //////////////////////////////////////////Install Printer////////////////////////////////////////////

                    ///////////////////Install B&W///////////////////

                    Microsoft.Win32.RegistryKey printerBnWExist = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Print\\Printers\\Canon Secure Print", false);

                    if (printerBnWExist == null)//registry key doesn't exist
                    {
                        pDll1.StartInfo.FileName = "rundll32.exe";
                        pDll1.StartInfo.Arguments = "printui.dll,PrintUIEntry /if /b \"Canon Secure Print\" /f \"" + home + "PrinterInstall\\x64\\Driver\\cns30ma64.inf\" /r acaduniflow.nyit.edu:canon_secure_print /m \"Canon Generic PS3 Driver2\" /z /u";
                        pDll1.Start();
                        pDll1.WaitForExit();
                    }
                    
                    ////////////////////Install Color///////////////////

                    Microsoft.Win32.RegistryKey printerColorExist = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Print\\Printers\\Canon Secure Print Color", false);

                    if (printerColorExist == null)//registry key doesn't exist
                    {
                        pDll1.StartInfo.FileName = "rundll32.exe";
                        pDll1.StartInfo.Arguments = "printui.dll,PrintUIEntry /if /b \"Canon Secure Print Color\" /f \"" + home + "PrinterInstall\\x64\\Driver\\cns30ma64.inf\" /r acaduniflow.nyit.edu:canon_secure_print_color /m \"Canon Generic PS3 Driver2\" /z /u";
                        pDll1.Start();
                        pDll1.WaitForExit();
                    }

                    

                    ///////////////////////////////////////////Configure Printers////////////////////////////////////////////////

                    pNet1.StartInfo.FileName = "net.exe";
                    pNet1.StartInfo.Arguments = "stop spooler";
                    pNet1.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                    pNet1.Start();
                    pNet1.WaitForExit();

                    ////////////////////Configure_B&W_Printer//////////////
                    
                    //if (printerBnWExist != null)//registry key exists
                    {
                        pReg1.StartInfo.FileName = "cmd.exe";
                        pReg1.StartInfo.Arguments = "/c reg import " + pConfigx64BnW + "";
                        pReg1.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                        pReg1.Start();
                        pReg1.WaitForExit();
                    }
                    
                    //////////////////Configure_Color_Printer///////////////

                    //if (printerColorExist != null)//registry key exists
                    {
                        pReg2.StartInfo.FileName = "cmd.exe";
                        pReg2.StartInfo.Arguments = "/c reg import " + pConfigx64Color + "";
                        pReg2.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                        pReg2.Start();
                        pReg2.WaitForExit();
                    }
                    
                    

                    pNet2.StartInfo.FileName = "net.exe";
                    pNet2.StartInfo.Arguments = "start spooler";
                    pNet2.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                    pNet2.Start();
                    pNet2.WaitForExit();

                    pMsi1.StartInfo.FileName = "Msiexec";
                    pMsi1.StartInfo.Arguments = "/i \"" + home + "PrinterInstall\\x64\\CanonPrintClient.msi\" /passive";
                    pMsi1.Start();
                    pMsi1.WaitForExit();

                    pMomClient.StartInfo.FileName = "momclnt.exe";
                    pMomClient.Start();
                    
                    //Console.WriteLine("This concludes the installation of NYIT's Wireless printing Services\nPress any key to continue");
                    //Console.ReadKey();
                    Application.EnableVisualStyles();
                    Application.Run(new Recommended_Restart());
                    

                }

                //Redirect
            }
            
            
            
            //else//32bit
            
                //Console.WriteLine("\n" + pInstallx32 + "\nPress any key to continue");
                //Console.ReadKey();
                
                 
                
            
        }
    }
}
