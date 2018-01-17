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

namespace expertmm {
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
		/// <summary>
		/// Load the programs for the extension of the program in the parameters
		/// </summary>
		/// <param name="programArgs"></param>
		/// <returns>Returns error message, otherwise null if status is good.</returns>
		public static string load(string[] programArgs) {
			lastErrorString=null;
			string errorString=null;
			try {
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
				sParticiple="adding known extension";
				
				//SharpDevelop 3.x:
				ProgChooser.AddExtension("sln");
				sParticiple="adding programs";
				bool IsGood=false;
				string sExeNow="";
				
				sExeNow=@"C:\Program Files (x86)\SharpDevelop\1.1\bin\SharpDevelop.exe";
				try {
					string sExeAlt=@"C:\Program Files\SharpDevelop\1.1\bin\SharpDevelop.exe";
					if (File.Exists(sExeAlt)) {
						sExeNow=sExeAlt;
					}
				}
				catch {}//dont care
				
				IsGood=false;
				if (File.Exists(sExeNow)) {
					ProgChooser.AddProg("sln",sExeNow,"SharpDevelop 1",1);
					IsGood=true;
				}
				else {//if (!IsGood) {
					//ALWAYS add, so that 1 key on keyboard opens version 1
					ProgChooser.AddProg("sln",sExeNow,"NONE (no SharpDevelop 1 found)",1);
				}
				
				
				sExeNow=@"C:\Program Files (x86)\SharpDevelop\2.2\bin\SharpDevelop.exe";
				try {
					string sExeAlt=@"C:\Program Files\SharpDevelop\2.2\bin\SharpDevelop.exe";
					if (File.Exists(sExeAlt)) {
						sExeNow=sExeAlt;
					}
				}
				catch {}//dont care
				
				IsGood=false;
				try {
					if (File.Exists(sExeNow)) {
						ProgChooser.AddProg("sln",sExeNow,"SharpDevelop 2",2);
						IsGood=true;
					}
				}
				catch {}//don't care
				if (!IsGood) {
					//ALWAYS add, so that 2 key on keyboard opens version 2
					ProgChooser.AddProg("sln",sExeNow,"NONE (no SharpDevelop 2 found)",2);
				}
				
				sExeNow="C:\\Program Files (x86)\\SharpDevelop\\3.0\\bin\\SharpDevelop.exe";
				try {
					string sExeAlt="E:\\PortableApps\\Programming\\SharpDevelop\\3.0\\bin\\SharpDevelop.exe";
					if (File.Exists(sExeAlt)) {
						sExeNow=sExeAlt;
					}
				}
				catch {
					
				}
				try {
					string sExeAlt="C:\\PortableApps\\Programming\\SharpDevelop\\3.0\\bin\\SharpDevelop.exe";
					if (File.Exists(sExeAlt)) {
						sExeNow=sExeAlt;
					}
				}
				catch {
					
				}
				try {
					string sExeAlt="C:\\Program Files\\SharpDevelop\\3.0\\bin\\SharpDevelop.exe";
					if (File.Exists(sExeAlt)) {
						sExeNow=sExeAlt;
					}
				}
				catch {
					
				}
				IsGood=false;
				
				try {
					if (File.Exists(sExeNow)) {
						ProgChooser.AddProg("sln",sExeNow,"SharpDevelop 3",3);
						IsGood=true;
					}
				}
				catch {}//don't care
				if (!IsGood) {
					//ALWAYS add, so that 3 key on keyboard opens version 3
					ProgChooser.AddProg("sln",sExeNow,"NONE (no SharpDevelop 3 found)",3);
				}
				
				
				//Visual C++:
				sExeNow="C:\\Program Files\\Microsoft Visual Studio 9.0\\Common7\\IDE\\VCExpress.exe";
				try {
					string sExeAlt="E:\\Program Files (x86)\\Microsoft Visual Studio 9.0\\Common7\\IDE\\VCExpress.exe";
					if (File.Exists(sExeAlt)) {
						sExeNow=sExeAlt;
					}
				}
				catch {
					
				}
				if (File.Exists(sExeNow)) ProgChooser.AddProg("sln",sExeNow,"Microsoft Visual Studio 9.0 (C++ 2008) Express Edition",9);
				else ProgChooser.AddProg("sln",sExeNow,"NONE (no Microsoft Visual Studio 9.0 (C++ 2008) Express Edition found)",9);
				
				try {
					string sExeAlt="E:\\Program Files (x86)\\Microsoft Visual Studio 9.0\\Common7\\IDE\\vcsexpress.exe";
					if (File.Exists(sExeAlt)) {
						sExeNow=sExeAlt;
					}
				}
				catch {
					
				}
				try {
					string sExeAlt="C:\\Program Files (x86)\\Microsoft Visual Studio 9.0\\Common7\\IDE\\vcsexpress.exe";
					if (File.Exists(sExeAlt)) {
						sExeNow=sExeAlt;
					}
				}
				catch {
					
				}
				try {
					string sExeAlt=@"C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\IDE\WDExpress.exe";
					if (File.Exists(sExeAlt)) {
						sExeNow=sExeAlt;
					}
				}
				catch {
					
				}
				if (File.Exists(sExeNow)) ProgChooser.AddProg("sln",sExeNow,"Microsoft Visual C# 2008 Express Edition",0);
				else ProgChooser.AddProg("sln",sExeNow,"NONE (no Microsoft Visual C# 2008 Express Edition found)",0);
				
				sExeNow=@"C:\Program Files (x86)\SharpDevelop\5.1\bin\SharpDevelop.exe";
				try {
					string sExeAlt=@"C:\Program Files\SharpDevelop\5.1\bin\SharpDevelop.exe";
					if (File.Exists(sExeAlt)) {
						sExeNow=sExeAlt;
					}
				}
				catch {
					
				}
				if (File.Exists(sExeNow)) ProgChooser.AddProg("sln",sExeNow,"SharpDevelop 5.1",5);
				else ProgChooser.AddProg("sln",sExeNow,"NONE (no SharpDevelop 5.1 found)",5);
					
			}
			catch (Exception exn) {
				ProgChooser.Error_WriteLine("Could not finish ProgChooserDefaults load for the following reason:"+exn.ToString());
			}			
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
		public static void AddProg(string sExt, string Prog_FullName, string Prog_Title, int index) {
			int iExt=IndexOfExt(sExt);
			if (iExt>=0) {
				extinfoarr[iExt].AddProg(Prog_FullName,Prog_Title, index);
			}
			else Console.Error.WriteLine("Error: "+Prog_FullName+" cannot be added--You first need to add the extension \""+sExt+"\"");
		}//end AddProg
	}//end ProgChooser
}//end ProgChooser namespace
