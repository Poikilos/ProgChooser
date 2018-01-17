/*
 * Created by SharpDevelop.
 * User: Jake Gustafson
 * Date: 6/29/2010
 * Time: 9:53 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;

namespace expertmm {
	/// <summary>
	/// Description of ExtensionInfo.
	/// </summary>
	public class ExtensionInfo {
		//const int proginfoarr_Max=10;//always 10 since user choise is numeric one-digit input
		//public int ProgramCount=0;
		public ProgInfo[] proginfoarr=new ProgInfo[10];//always 10 since user choise is numeric one-digit input
		/// <summary>
		/// not used
		/// </summary>
		public string sDescription;
		/// <summary>
		/// 3-letter extension
		/// </summary>
		public string sExt;
		public ExtensionInfo() {
		}
		public ProgInfo[] getProgInfoArray_ByRef() {
			return proginfoarr;
		}
		public int getFreeIndex() {
			int returnIndex=-1;
			for (int index=0; index<proginfoarr.Length; index++) {
				if (proginfoarr[index]==null) {
					returnIndex = index;
					break;
				}
			}
			return returnIndex;
		}
		public int getProgIndexFromKeyChar(char thisChar) {
			int returnIndex = -1;
			for (int index=1; index<proginfoarr.Length; index++) {
				if (proginfoarr[index]!=null && proginfoarr[index].hotkeyChar==thisChar) {
					returnIndex=index;
					break;
				}
			}
			return returnIndex;
		}
		public bool isSlotUsed(int index) {
			return ( (index>=0&&index<proginfoarr.Length) ? (proginfoarr[index]!=null) : false );
		}
		
		public void showChoiceToArrayList(ArrayList thisAL, int index) {
			try {
				if (thisAL==null) thisAL=new ArrayList();
				if (proginfoarr[index]!=null) {
					while (thisAL.Count<index+1) {
						thisAL.Add("");
					}
					string item_string=((index).ToString()+". "+proginfoarr[index].Title);
					//thisAL.Add(item_string);
					thisAL[index]=item_string;
				}
				else {
					Console.Error.WriteLine("showChoiceToArrayList error: tried to get null item proginfoarr["+index.ToString()+"]");
				}
			}
			catch (Exception exn) {
				Console.Error.WriteLine("Could not finish showChoiceToArrayList: "+exn.ToString());
			}
		}
		public void showChoiceToConsoleOut(int index) {
			if (proginfoarr[index]!=null) Console.WriteLine((index).ToString()+". "+proginfoarr[index].Title);
		}
		
		public void showChoicesToConsoleOut() {
			int iProg=1;
			for (iProg=1; iProg<proginfoarr.Length; iProg++) {
				showChoiceToConsoleOut(iProg);
			}
			showChoiceToConsoleOut(0);
		}
		public ArrayList showChoicesToArrayList() {
			int iProg=1;
			ArrayList thisAL=new ArrayList();
			for (iProg=0; iProg<proginfoarr.Length; iProg++) {
				Console.WriteLine("proginfoarr["+iProg.ToString()+"]"+((proginfoarr[iProg]!=null)?".Fullname:"+proginfoarr[iProg].FullName:":null"));
				showChoiceToArrayList(thisAL, iProg);
			}
			return thisAL;
		}
//		public bool AddProg(string Prog_FullName, string Prog_Title) {
//			return AddProg(Prog_FullName, Prog_Title, getFreeIndex());
//		}
		public bool AddProg(string Prog_FullName, string Prog_Title, int index) {
			bool IsAdded=false;
			if (index<proginfoarr.Length) {
				if (proginfoarr[index]==null) proginfoarr[index]=new ProgInfo();
				proginfoarr[index].FullName=Prog_FullName;
				proginfoarr[index].Title=Prog_Title;
				proginfoarr[index].hotkeyChar=index.ToString()[0];
				//ProgramCount++;
				IsAdded=true;
			}
			else {
				IsAdded=false;
				Console.Error.WriteLine("PROGRAMMING ERROR: index "+index.ToString()+" is already used.");
				Console.Error.WriteLine("press any key to continue...");
			}
			return IsAdded;
		}
	}//end ExtensionInfo
}//end namespace
