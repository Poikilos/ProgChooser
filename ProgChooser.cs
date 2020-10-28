/*
 * Created by SharpDevelop.
 * User: Jake Gustafson
 * Date: 6/29/2010
 * Time: 9:57 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics; //Process etc
using System.IO; //File etc
using System.Collections;

namespace ProgChooser {
	/// <summary>
	/// ProgChooser
	/// </summary>
	public class ProgChooser {
		public static string openingFileExt="";
		public static bool IsAnswered=false;
		public static int iParam0=0;//it would be 1 in C++
		private static ExtensionInfo[] extinfoarr=new ExtensionInfo[100];
		private static int extinfoarr_Used=0;
		private static string openedFileFullName=null;
		private static string lastErrorString=null;
		private static int fileCount=0;//should be one (others are skipped)
		private static bool isLaunched=false;
		private static string sParticiple="";
		private static ExtensionInfo extinfoNow=null;
		public ProgChooser() {
		}
		public static string getExtensionString() {
			string extString = "";
			if (openingFileExt != null) extString=openingFileExt;
			return extString;
		}
		public static int getProgIndexFromKeyChar(char thisChar) {
			int foundIndex=-1;
			if (extinfoNow!=null) foundIndex=extinfoNow.getProgIndexFromKeyChar(thisChar);
			return foundIndex;
		}
		public static void showChoicesToConsoleOut() {
			if (extinfoNow!=null) extinfoNow.showChoicesToConsoleOut();
		}
		public static ArrayList showChoicesToArrayList() {
			ArrayList thisAL=null;
			if (extinfoNow!=null) thisAL=extinfoNow.showChoicesToArrayList();
			return thisAL;
		}
		public static bool getExtensionIsKnown() {
			return extinfoNow!=null;
		}
		public static int getFileCount() {
			return fileCount;
		}
		public static string getOpenedFileFullName() {
			return openedFileFullName;
		}
		public static string getError() {
			return lastErrorString;
		}
		public static bool launchProgram(int iProgIndex) {
			//isLaunched=false;
			
			lastErrorString=null;
			if (extinfoNow!=null) {
				if (extinfoNow.isSlotUsed(iProgIndex)) {
					try {
						ProgChooser.IsAnswered=true;
						//string sCommand=proginfoarrNow[iProgIndex].FullName+" \""+programArgs[iParam0]+"\"";
						//ProcessStartInfo psiNow=new ProcessStartInfo(proginfoarrNow[iProgIndex].FullName,programArgs[iParam0]);
						//Process ps=Process.Start(psiNow);
			            Process ps=new Process();
			            ps.StartInfo.FileName=extinfoNow.proginfoarr[iProgIndex].FullName;
			            if (openedFileFullName.Contains(" ")
			                && openedFileFullName.Length>2
			                && ( openedFileFullName[0]!='"' || openedFileFullName[openedFileFullName.Length-1]!='"' )  ) {
			            	ps.StartInfo.Arguments="\""+openedFileFullName+"\"";
			            }
			            else ps.StartInfo.Arguments=openedFileFullName;
			            ps.Start();
			            isLaunched=true;
					}
					catch (Exception exn) {
						ProgChooser.Error_WriteLine("launchProgram could not finish for the following reason: "+exn.ToString());
					}
				}
				else {
					ProgChooser.Error_WriteLine("launchProgram could not find anything in slot "+iProgIndex.ToString());
				}
			}
			else {
				ProgChooser.Error_WriteLine("Sorry, ProgChooser has no info for that file extension");
			}
			return ProgChooser.isLaunched;
		}//end launchProgram
		private static void Error_WriteLine(string msg) {
			lastErrorString=msg;
			Console.Error.WriteLine(msg);
		}
		private static void setOpenedFileFullNameFrom(string[] programArgs) {
			openedFileFullName=null;
			for (int index=ProgChooser.iParam0; index<programArgs.Length; index++) {
				openedFileFullName=programArgs[index];
				sParticiple="getting current extension";
				int iLastDot=openedFileFullName.LastIndexOf(".");
				if (iLastDot>=0) {
					if (iLastDot+1<openedFileFullName.Length) ProgChooser.openingFileExt=openedFileFullName.Substring(iLastDot+1);
					else ProgChooser.openingFileExt="";
				}
				else ProgChooser.openingFileExt="";
				extinfoNow=ProgChooser.getExtInfo_ByRef(ProgChooser.openingFileExt);
				break;
			}
		}
		public static bool AddChoice(string ext, string[] paths, string caption, int hotkeyI) {
			string[] captions = new string[paths.Length];
			for (int index=0; index<paths.Length; index++) {
				captions[index] = caption;
			}
			return AddChoice(ext, paths, captions, hotkeyI);
		}
		public static bool AddChoice(string ext, string[] paths, string[] captions, int hotkeyI) {
			string sExeNow = null;
			int foundI = -1;
			Console.Error.WriteLine("checking for program for " + ext);
			for (int index=0; index<paths.Length; index++) {
				try {
					if (File.Exists(paths[index])) {
						sExeNow = paths[index];
						Console.Error.WriteLine("found " + sExeNow);
						foundI = index;
					}
					else {
						Console.Error.WriteLine("no " + paths[index]);
					}
				}
				catch {
					if (paths[index] != null) {
						Console.Error.WriteLine("Path "
						                        + index.ToString()
						                        + " is bad for "
						                        + ext + ": \""
						                        + paths[index] + "\"");
					}
					else {
						Console.Error.WriteLine("Path "
						                        + index.ToString()
						                        + " is bad for "
						                        + ext);
					}
				} // if path is bad, don't care
			}
			if (sExeNow != null) {
				ProgChooser.ForceAddChoice("sln", sExeNow, captions[foundI], hotkeyI);
			}
			else {
				// ALWAYS add it, so that the number key corresponding
				// to kotkeyI opens the matching sequential version.
				ProgChooser.ForceAddChoice("sln", paths[0], "NONE (no " + captions[0] + " found)", hotkeyI);
			}
			return sExeNow != null;
		}
		/// <summary>
		/// Load the programs for the extension of the program in the parameters
		/// </summary>
		/// <param name="programArgs"></param>
		/// <returns>Returns error message, otherwise null if status is good.</returns>
		public static string load(string[] programArgs) {
			lastErrorString = null;
			string errorString = null;
			//try {
			ProgChooser.IsAnswered=false;
			//int proginfoarrNow_Max=10;
			//ProgInfo[] proginfoarrNow=new ProgInfo[proginfoarrNow_Max];
			//int extinfoNow.ProgramCount=0;
			//proginfoarrNow[extinfoNow.ProgramCount].Title="SharpDevelop 3.0";
			//proginfoarrNow[extinfoNow.ProgramCount].FullName="C:\\PortableApps\\Programming\\SharpDevelop\\3.0\\bin\\SharpDevelop.exe";
			//extinfoNow.ProgramCount++;
			//proginfoarrNow[extinfoNow.ProgramCount].Title="Microsoft Visual C++ 2008 Express Edition";
			//proginfoarrNow[extinfoNow.ProgramCount].FullName="C:\\Program Files\\Microsoft Visual Studio 9.0\\Common7\\IDE\\VCExpress.exe";
			//extinfoNow.ProgramCount++;
			
			/// ADD PROGRAMS BY EXTENSION ///
			sParticiple = "adding known extension";
			
			//SharpDevelop 3.x:
			ProgChooser.AddExtension("sln");
			sParticiple = "adding programs";
			
			Console.Error.WriteLine(sParticiple);
			
			AddChoice(
				"sln",
				new[] {
					@"C:\Program Files (x86)\SharpDevelop\1.1\bin\SharpDevelop.exe",
					@"C:\Program Files\SharpDevelop\1.1\bin\SharpDevelop.exe"
				},
				"SharpDevelop 1",
				1
			);
			AddChoice(
				"sln",
				new[] {
					@"C:\Program Files (x86)\SharpDevelop\2.2\bin\SharpDevelop.exe",
					@"C:\Program Files\SharpDevelop\2.2\bin\SharpDevelop.exe"
				},
				"SharpDevelop 2",
				2
			);
			AddChoice(
				"sln",
				new[] {
					"C:\\Program Files (x86)\\SharpDevelop\\3.0\\bin\\SharpDevelop.exe",
					"E:\\PortableApps\\Programming\\SharpDevelop\\3.0\\bin\\SharpDevelop.exe",
					"C:\\PortableApps\\Programming\\SharpDevelop\\3.0\\bin\\SharpDevelop.exe",
					"C:\\Program Files\\SharpDevelop\\3.0\\bin\\SharpDevelop.exe"
				},
				"SharpDevelop 3",
				3
			);
			AddChoice(
				"sln",
				new[] {
					@"C:\Program Files (x86)\SharpDevelop\4.4\bin\SharpDevelop.exe",
					@"C:\Program Files\SharpDevelop\4.4\bin\SharpDevelop.exe",
					"E:\\PortableApps\\Programming\\SharpDevelop\\4.4\\bin\\SharpDevelop.exe",
					"C:\\PortableApps\\Programming\\SharpDevelop\\4.4\\bin\\SharpDevelop.exe"
				},
				"SharpDevelop 4",
				4
			);
			AddChoice(
				"sln",
				new[] {
					@"C:\Program Files (x86)\SharpDevelop\5.1\bin\SharpDevelop.exe",
					@"C:\Program Files\SharpDevelop\5.1\bin\SharpDevelop.exe"
				},
				"SharpDevelop 5.1",
				5
			);
			AddChoice(
				"sln",
				new[] {
					"C:\\Program Files\\Microsoft Visual Studio 9.0\\Common7\\IDE\\VCExpress.exe",
					"E:\\Program Files (x86)\\Microsoft Visual Studio 9.0\\Common7\\IDE\\VCExpress.exe"
				},
				"Microsoft Visual Studio 9.0 (C++ 2008) Express Edition",
				9
			);
			AddChoice(
				"sln",
				new[] {
					"E:\\Program Files (x86)\\Microsoft Visual Studio 9.0\\Common7\\IDE\\vcsexpress.exe",
					"C:\\Program Files (x86)\\Microsoft Visual Studio 9.0\\Common7\\IDE\\vcsexpress.exe",
					@"C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\IDE\WDExpress.exe"
				},
				new[] {
					"Microsoft Visual C# 2008 Express Edition",
					"Microsoft Visual C# 2008 Express Edition",
					"Microsoft Visual C# 2012 Express Edition"
				},
				0
			);
			//}
			//catch (Exception exn) {
			//	ProgChooser.Error_WriteLine("Could not finish ProgChooserDefaults load for the following reason:"+exn.ToString());
			//}			
			ProgChooser.setOpenedFileFullNameFrom(programArgs);
			
//			if (ProgChooser.getOpenedFileFullName()!=null) {
//				
//			}
//			//NOTE: else setOpendFileFullNameFrom already should have set lastErrorString
			return errorString;
		}
		public static ExtensionInfo getExtInfo_ByRef(int iExt) {
			ExtensionInfo extinfoReturn=null;
			if (iExt>=0&&iExt<extinfoarr_Used) {
				extinfoReturn=extinfoarr[iExt];
			}
			else Console.Error.WriteLine("Error: ProgChooser has no extension info at index "+iExt.ToString());
			return extinfoReturn;
		}
		public static ExtensionInfo getExtInfo_ByRef(string sExt) {
			ExtensionInfo extinfoReturn=null;
			int iExt=IndexOfExt(sExt);
			if (iExt>=0) {
				extinfoReturn=extinfoarr[iExt];
			}
			else Console.Error.WriteLine("Error: ProgChooser has no info for extension \""+sExt+"\"");
			return extinfoReturn;
		}
		public static void AddExtension(string sExt) {//, string sDescription, byte[] byarrStartsWith) {
			if (extinfoarr_Used>=extinfoarr.Length) {
				ExtensionInfo[] extinfoarrNew=new ExtensionInfo[extinfoarr.Length+extinfoarr.Length/2+1];
				for (int iNow=0; iNow<extinfoarrNew.Length; iNow++) {
					if (iNow<extinfoarr_Used) {
						extinfoarrNew[iNow]=extinfoarr[iNow];
					}
					else extinfoarrNew[iNow]=null;
				}//end for iNow (copying old data to resized array)
				extinfoarr=extinfoarrNew;
			}
			if (extinfoarr_Used<extinfoarr.Length) {
				if (extinfoarr[extinfoarr_Used]==null) extinfoarr[extinfoarr_Used]=new ExtensionInfo();
				extinfoarr[extinfoarr_Used].sExt=sExt;
				extinfoarr_Used++;
			}
			else {
				Console.Error.WriteLine("Error: Extension array could not be expanded--memory is probably gone.");
			}
		}//end AddExtension
		public static int IndexOfExt(string sExt) {
			int iReturn=-1;
			string sExt_ToLower=sExt.ToLower();
			for (int iNow=0; iNow<extinfoarr_Used; iNow++) {
				if (extinfoarr[iNow].sExt.ToLower()==sExt_ToLower) {
					iReturn=iNow;
					break;
				}
			}
			return iReturn;
		}//end IndexOfExt
		public static void ForceAddChoice(string sExt, string Prog_FullName, string Prog_Title, int index) {
			int iExt = IndexOfExt(sExt);
			if (iExt >= 0) {
				extinfoarr[iExt].ForceAddChoice(Prog_FullName, Prog_Title, index);
			}
			else Console.Error.WriteLine("Error: "+Prog_FullName+" cannot be added--You first need to add the extension \""+sExt+"\"");
		}//end ForceAddChoice
	}//end ProgChooser
}//end ProgChooser namespace
