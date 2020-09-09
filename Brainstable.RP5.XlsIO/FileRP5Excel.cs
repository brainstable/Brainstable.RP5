using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using FlexCel.XlsAdapter;

namespace Brainstable.RP5.XlsIO
{
    
    
    internal class FileRP5Excel
    {
        /*

        protected override string[] CreateArrayByLine(string fileName)
        {
            XlsFile xlsFile = new XlsFile();
            xlsFile.Open(fileName);
            xlsFile.ActiveSheet = 1;

            List<string> list = new List<string>();

            int row = 1;


            return list.ToArray();
        }

        public Dictionary<long, ObservationPoint> LoadFile(string pathFile)
        {
            Dictionary<long, ObservationPoint> dict = new Dictionary<long, ObservationPoint>();

            XlsFile xlsFile = new XlsFile();
            xlsFile.Open(pathFile);
            xlsFile.ActiveSheet = 1;

            string[] arrayIO = CreateArrayFromObservationInfo(xlsFile);
            ObservationInfo = ObservationInfo.CreateObservationInfo(arrayIO);

            Hashtable hashtable = CreateHashtable(xlsFile);

            int row = 8;
            while (xlsFile.GetCellValue(row, 1) != null)
            {
                ObservationPoint p = CreatePointObservation(xlsFile, row, hashtable);
                if (p != null)
                {
                    if (!dict.ContainsKey(p.Id))
                    {
                        dict[p.Id] = p;
                    }
                }
                row++;
            }
            return dict;
        }

        private Hashtable CreateHashtable(XlsFile xlsFile)
        {
            Hashtable hashtable = new Hashtable();
            int col = 1;
            while (xlsFile.GetCellValue(7, col) != null)
            {
                string key = xlsFile.GetCellValue(7, col).ToString().ToUpper();
                if (!hashtable.ContainsKey(key))
                    hashtable[key] = col;
                col++;
            }

            return hashtable;
        }

        private string[] CreateArrayFromObservationInfo(XlsFile xlsFile)
        {
            string[] arr = null;

            try
            {
                arr = new string[5];
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = xlsFile.GetCellValue(i + 1, 1) == null ? "" : xlsFile.GetCellValue(i + 1, 1).ToString();
                }
            }
            catch (Exception)
            {

                throw;
            }



            return arr;
        }

        private ObservationPoint CreatePointObservation(XlsFile xlsFile, int row, Hashtable hashtable)
        {
            ObservationPoint p = null;

            try
            {
                p = new ObservationPoint();
                p.LoadDateTimeFromString(xlsFile.GetCellValue(row, 1).ToString());
                if (hashtable.ContainsKey("T"))
                    p.T = xlsFile.GetCellValue(row, (int)hashtable["T"]) != null ? xlsFile.GetCellValue(row, (int)hashtable["T"]).ToString() : "";
                if (hashtable.ContainsKey("PO"))
                    p.PO = xlsFile.GetCellValue(row, (int)hashtable["PO"]) != null ? xlsFile.GetCellValue(row, (int)hashtable["PO"]).ToString() : "";
                if (hashtable.ContainsKey("P0"))
                    p.PO = xlsFile.GetCellValue(row, (int)hashtable["P0"]) != null ? xlsFile.GetCellValue(row, (int)hashtable["P0"]).ToString() : "";
                if (hashtable.ContainsKey("P"))
                    p.P = xlsFile.GetCellValue(row, (int)hashtable["P"]) != null ? xlsFile.GetCellValue(row, (int)hashtable["P"]).ToString() : "";
                if (hashtable.ContainsKey("PA"))
                    p.PA = xlsFile.GetCellValue(row, (int)hashtable["PA"]) != null ? xlsFile.GetCellValue(row, (int)hashtable["PA"]).ToString() : "";
                if (hashtable.ContainsKey("U"))
                    p.U = xlsFile.GetCellValue(row, (int)hashtable["U"]) != null ? xlsFile.GetCellValue(row, (int)hashtable["U"]).ToString() : "";
                if (hashtable.ContainsKey("DD"))
                    p.DD = xlsFile.GetCellValue(row, (int)hashtable["DD"]) != null ? xlsFile.GetCellValue(row, (int)hashtable["DD"]).ToString() : "";
                if (hashtable.ContainsKey("FF"))
                    p.FF = xlsFile.GetCellValue(row, (int)hashtable["FF"]) != null ? xlsFile.GetCellValue(row, (int)hashtable["FF"]).ToString() : "";
                if (hashtable.ContainsKey("FF10"))
                    p.FF10 = xlsFile.GetCellValue(row, (int)hashtable["FF10"]) != null ? xlsFile.GetCellValue(row, (int)hashtable["FF10"]).ToString() : "";
                if (hashtable.ContainsKey("FF3"))
                    p.FF3 = xlsFile.GetCellValue(row, (int)hashtable["FF3"]) != null ? xlsFile.GetCellValue(row, (int)hashtable["FF3"]).ToString() : "";
                if (hashtable.ContainsKey("N"))
                    p.N = xlsFile.GetCellValue(row, (int)hashtable["N"]) != null ? xlsFile.GetCellValue(row, (int)hashtable["N"]).ToString() : "";
                if (hashtable.ContainsKey("WW"))
                    p.WW = xlsFile.GetCellValue(row, (int)hashtable["WW"]) != null ? xlsFile.GetCellValue(row, (int)hashtable["WW"]).ToString() : "";
                if (hashtable.ContainsKey("W1"))
                    p.W1 = xlsFile.GetCellValue(row, (int)hashtable["W1"]) != null ? xlsFile.GetCellValue(row, (int)hashtable["W1"]).ToString() : "";
                if (hashtable.ContainsKey("W2"))
                    p.W2 = xlsFile.GetCellValue(row, (int)hashtable["W2"]) != null ? xlsFile.GetCellValue(row, (int)hashtable["W2"]).ToString() : "";
                if (hashtable.ContainsKey("W'W'"))
                    p.W_W_ = xlsFile.GetCellValue(row, (int)hashtable["W'W'"]) != null ? xlsFile.GetCellValue(row, (int)hashtable["W'W'"]).ToString() : "";
                if (hashtable.ContainsKey("TN"))
                    p.Tn = xlsFile.GetCellValue(row, (int)hashtable["TN"]) != null ? xlsFile.GetCellValue(row, (int)hashtable["TN"]).ToString() : "";
                if (hashtable.ContainsKey("TX"))
                    p.Tx = xlsFile.GetCellValue(row, (int)hashtable["TX"]) != null ? xlsFile.GetCellValue(row, (int)hashtable["TX"]).ToString() : "";
                if (hashtable.ContainsKey("CL"))
                    p.Cl = xlsFile.GetCellValue(row, (int)hashtable["CL"]) != null ? xlsFile.GetCellValue(row, (int)hashtable["CL"]).ToString() : "";
                if (hashtable.ContainsKey("C"))
                    p.C = xlsFile.GetCellValue(row, (int)hashtable["C"]) != null ? xlsFile.GetCellValue(row, (int)hashtable["C"]).ToString() : "";
                if (hashtable.ContainsKey("NH"))
                    p.Nh = xlsFile.GetCellValue(row, (int)hashtable["NH"]) != null ? xlsFile.GetCellValue(row, (int)hashtable["NH"]).ToString() : "";
                if (hashtable.ContainsKey("H"))
                    p.H = xlsFile.GetCellValue(row, (int)hashtable["H"]) != null ? xlsFile.GetCellValue(row, (int)hashtable["H"]).ToString() : "";
                if (hashtable.ContainsKey("CM"))
                    p.Cm = xlsFile.GetCellValue(row, (int)hashtable["CM"]) != null ? xlsFile.GetCellValue(row, (int)hashtable["CM"]).ToString() : "";
                if (hashtable.ContainsKey("CH"))
                    p.Ch = xlsFile.GetCellValue(row, (int)hashtable["CH"]) != null ? xlsFile.GetCellValue(row, (int)hashtable["CH"]).ToString() : "";
                if (hashtable.ContainsKey("VV"))
                    p.VV = xlsFile.GetCellValue(row, (int)hashtable["VV"]) != null ? xlsFile.GetCellValue(row, (int)hashtable["VV"]).ToString() : "";
                if (hashtable.ContainsKey("TD"))
                    p.Td = xlsFile.GetCellValue(row, (int)hashtable["TD"]) != null ? xlsFile.GetCellValue(row, (int)hashtable["TD"]).ToString() : "";
                if (hashtable.ContainsKey("RRR"))
                    p.RRR = xlsFile.GetCellValue(row, (int)hashtable["RRR"]) != null ? xlsFile.GetCellValue(row, (int)hashtable["RRR"]).ToString() : "";
                if (hashtable.ContainsKey("TR"))
                    p.TR = xlsFile.GetCellValue(row, (int)hashtable["TR"]) != null ? xlsFile.GetCellValue(row, (int)hashtable["TR"]).ToString() : "";
                if (hashtable.ContainsKey("E"))
                    p.E = xlsFile.GetCellValue(row, (int)hashtable["E"]) != null ? xlsFile.GetCellValue(row, (int)hashtable["E"]).ToString() : "";
                if (hashtable.ContainsKey("TG"))
                    p.Tg = xlsFile.GetCellValue(row, (int)hashtable["TG"]) != null ? xlsFile.GetCellValue(row, (int)hashtable["TG"]).ToString() : "";
                if (hashtable.ContainsKey("E'"))
                    p.Es = xlsFile.GetCellValue(row, (int)hashtable["E'"]) != null ? xlsFile.GetCellValue(row, (int)hashtable["E'"]).ToString() : "";
                if (hashtable.ContainsKey("SSS"))
                    p.SSS = xlsFile.GetCellValue(row, (int)hashtable["SSS"]) != null ? xlsFile.GetCellValue(row, (int)hashtable["SSS"]).ToString() : "";
            }
            catch (Exception)
            {
                // TODO: WriteLog
            }

            return p;
        }

        */
    }
        
}