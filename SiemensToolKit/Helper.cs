using Sharp7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiemensToolKit
{
    public static class Helper
    {
        public static bool ReadState(this Symbol symbol,IOBuffer buffer)
        {
            if(symbol.Type== DataType.I)
            {
                return symbol.State = ReadBitFromBuffer(symbol.Index, symbol.SubIndex, buffer.InputBuffer);
            }
            if (symbol.Type == DataType.Q)
            {
                return symbol.State = ReadBitFromBuffer(symbol.Index, symbol.SubIndex, buffer.OutputBuffer);
            }
            if (symbol.Type == DataType.M)
            {
                return symbol.State= ReadBitFromBuffer(symbol.Index, symbol.SubIndex, buffer.MbitBuffer);
            }
            return false;
        }
        public static IOBuffer ReadSymbols(this SymbolList symbolList, S7Client Client, int bufferSize = 16384)
        {
            IOBuffer iob = new IOBuffer().Initialize(bufferSize);
            foreach(var symbol in symbolList.Symbols)
            {
                //input symbol
                if (symbol.Address.ToUpper().Contains("I") || symbol.Address.ToUpper().Contains("E") && !symbol.Address.ToUpper().Contains("P") && !symbol.Address.ToUpper().Contains("D"))
                {
                    iob.InputBuffer = GetAllInputs(Client, ref iob.I_result);
                    symbol.State = ReadBitFromBuffer(symbol.Index, symbol.SubIndex, iob.InputBuffer);
                }
                //output symbol
                else if (symbol.Address.ToUpper().Contains("Q") || symbol.Address.ToUpper().Contains("A") && !symbol.Address.ToUpper().Contains("P") && !symbol.Address.ToUpper().Contains("D"))
                {
                    iob.OutputBuffer = GetAllOutputs(Client, ref iob.O_result);
                    symbol.State = ReadBitFromBuffer(symbol.Index, symbol.SubIndex, iob.OutputBuffer);
                }
                //mbit symbol
                else if(symbol.Address.ToUpper().Contains("M") && !symbol.Address.ToUpper().Contains("P") && !symbol.Address.ToUpper().Contains("D"))
                {
                    iob.MbitBuffer = GetAllMbits(Client, ref iob.O_result);
                    symbol.State = ReadBitFromBuffer(symbol.Index, symbol.SubIndex, iob.MbitBuffer);
                }
            }
            return iob;
        }
        public static IOBuffer ReadIO(this S7Client Client, int bufferSize = 16384)
        {
            IOBuffer iob = new IOBuffer().Initialize(bufferSize);
            iob.InputBuffer = GetAllInputs(Client, ref iob.I_result);
            iob.OutputBuffer = GetAllOutputs(Client, ref iob.O_result);
            return iob;
        }
        public static string ReadInput (this S7Client Client, int address, int subAddress, int bufferSize = 16384)
        {
            string result = "Error";
            try
            {
                var buffer = Client.GetAllInputs(ref result,bufferSize);
                var bit = ReadBitFromBuffer(address, subAddress, buffer);
            }
            catch { }
            return result;
        }
        public static string ReadOutput(this S7Client Client, int address, int subAddress, int bufferSize = 16384)
        {
            string result = "Error";
            try
            {
                var buffer = Client.GetAllOutputs(ref result,bufferSize);
                var bit = ReadBitFromBuffer(address, subAddress, buffer);
            }
            catch { }
            return result;
        }
        public static byte[] GetAllInputs(this S7Client Client, ref string result,int bufferSize=16384)
        {
            byte[] buffer = new byte[bufferSize];            
            result = Client.ShowResult(
                Client.EBRead(0, 16384, buffer)
                );
            return buffer;
        }
        public static byte[] GetAllOutputs(this S7Client Client, ref string result, int bufferSize = 16384)
        {
            byte[] buffer = new byte[bufferSize];
            result = Client.ShowResult(
                Client.ABRead(0, 16384, buffer)
                );
            return buffer;
        }
        public static byte[] GetAllMbits(this S7Client Client, ref string result, int bufferSize = 16384)
        {
            byte[] buffer = new byte[bufferSize];
            result = Client.ShowResult(
                Client.MBRead(0, 16384, buffer)
                );
            return buffer;
        }
        public static bool ReadBitFromBuffer(int address, int subAddress, byte[] buffer)
        {
            try
            {
                return S7.GetBitAt(buffer, address, subAddress);
            }
            catch { }
            return false;
        }
        public static string HexByte(byte B)
        {
            string Result = Convert.ToString(B, 16);
            if (Result.Length < 2)
                Result = "0" + Result;
            return "0x" + Result;
        }

        public static string HexWord(ushort W)
        {
            string Result = Convert.ToString(W, 16);
            while (Result.Length < 4)
                Result = "0" + Result;
            return "0x" + Result;
        }
        public static string ShowResult(this S7Client Client, int Result)
        {
            string ret = "";
            // This function returns a textual explaination of the error code
            ret = Client.ErrorText(Result);
            if (Result == 0)
                ret = " (" + Client.ExecutionTime.ToString() + " ms)";
            return ret;
        }

        //S7Client Override
        /*
        public static string GetBlockInfo(this S7Client Client, int blockNumber, int blockType)
        {
            S7Client.S7BlockInfo BI = new S7Client.S7BlockInfo();
            int[] BlockType =
            {
                S7Client.Block_OB,
                S7Client.Block_DB,
                S7Client.Block_SDB,
                S7Client.Block_FC,
                S7Client.Block_SFC,
                S7Client.Block_FB,
                S7Client.Block_SFB
            };
            txtBI.Text = "";
            int Result = Client.GetAgBlockInfo(BlockType[blockType], blockNumber, ref BI);
            ShowResult(Result);
            if (Result == 0)
            {
                // Here a more descriptive Block Type, Block lang and so on, are needed, 
                // but I'm too lazy, do it yourself.
                txtBI.Text = txtBI.Text + "Block Type    : " + HexByte((byte)BI.BlkType) + (char)13 + (char)10;
                txtBI.Text = txtBI.Text + "Block Number  : " + Convert.ToString(BI.BlkNumber) + (char)13 + (char)10;
                txtBI.Text = txtBI.Text + "Block Lang    : " + HexByte((byte)BI.BlkLang) + (char)13 + (char)10;
                txtBI.Text = txtBI.Text + "Block Flags   : " + HexByte((byte)BI.BlkFlags) + (char)13 + (char)10;
                txtBI.Text = txtBI.Text + "MC7 Size      : " + Convert.ToString(BI.MC7Size) + (char)13 + (char)10;
                txtBI.Text = txtBI.Text + "Load Size     : " + Convert.ToString(BI.LoadSize) + (char)13 + (char)10;
                txtBI.Text = txtBI.Text + "Local Data    : " + Convert.ToString(BI.LocalData) + (char)13 + (char)10;
                txtBI.Text = txtBI.Text + "SBB Length    : " + Convert.ToString(BI.SBBLength) + (char)13 + (char)10;
                txtBI.Text = txtBI.Text + "Checksum      : " + HexWord((ushort)BI.CheckSum) + (char)13 + (char)10;
                txtBI.Text = txtBI.Text + "Version       : 0." + Convert.ToString(BI.Version) + (char)13 + (char)10;
                txtBI.Text = txtBI.Text + "Code Date     : " + BI.CodeDate + (char)13 + (char)10;
                txtBI.Text = txtBI.Text + "Intf.Date     : " + BI.IntfDate + (char)13 + (char)10;
                txtBI.Text = txtBI.Text + "Author        : " + BI.Author + (char)13 + (char)10;
                txtBI.Text = txtBI.Text + "Family        : " + BI.Family + (char)13 + (char)10;
                txtBI.Text = txtBI.Text + "Header        : " + BI.Header;
            }
        }
        */
    }
    [Serializable]
    public class SymbolList
    {
        public void Import()
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.Filter = "S7 SDF|*.sdf|All Files| *.*";
            var result = ofd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                using (var file = System.IO.File.OpenRead(ofd.FileName))
                using (var reader = new System.IO.StreamReader(file))
                {
                    Symbols = new List<Symbol>();
                    do
                    {
                        string line = reader.ReadLine();
                        string[] raw = line.Split(',');

                        Symbol s = new Symbol();
                        s.Name = raw[0].Replace("\"", "");
                        s.Address = raw[1].Replace("\"", "");
                        //parse out address
                        char[] alpha = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', ' ' };
                        string t = s.Address;
                        foreach (var a in alpha)
                        {
                            t = t.Replace(a.ToString(), "");
                        }
                        var tmp = t.Split('.');
                        s.Index = Convert.ToInt32(tmp[0]);
                        if (tmp.Count() > 1)
                        {
                            s.SubIndex = Convert.ToInt32(tmp[1]);
                        }
                        var dt = raw[2].Replace("\"", "");

                        if (dt.ToUpper().Contains("FC"))
                        {
                            s.Type = DataType.FC;
                        }
                        else
                            if (dt.ToUpper().Contains("OB"))
                        {
                            s.Type = DataType.OB;
                        }
                        else if (dt.ToUpper().Contains("DB"))
                        {
                            s.Type = DataType.DB;
                        }
                        else
                            if (dt.ToUpper().Contains("SFC"))
                        {
                            s.Type = DataType.SFC;
                        }
                        else
                            if (dt.ToUpper().Contains("WORD"))
                        {
                            s.Type = DataType.Word;
                        }
                        else
                            if (dt.ToUpper().Contains("DWORD"))
                        {
                            s.Type = DataType.Dword;
                        }
                        else
                            if (dt.ToUpper().Contains("REAL"))
                        {
                            s.Type = DataType.Real;
                        }
                        else
                            if (dt.ToUpper().Contains("COUNTER"))
                        {
                            s.Type = DataType.Counter;
                        }
                        else
                            if (dt.ToUpper().Contains("TIMER"))
                        {
                            s.Type = DataType.Timer;
                        }
                        else
                            if (dt.ToUpper().Contains("BYTE"))
                        {
                            s.Type = DataType.Byte;
                        }
                        else
                            if (dt.ToUpper().Contains("UDT"))
                        {
                            s.Type = DataType.UDT;
                        }
                        else
                        {
                            s.Type = DataType.None;
                        }

                        if (dt.ToUpper().Contains("BOOL"))
                        {
                            s.Type = DataType.Bool;
                        }
                        if (s.Address.ToUpper().Contains("I") || s.Address.ToUpper().Contains("E") && !s.Address.ToUpper().Contains("P") && !s.Address.ToUpper().Contains("D"))
                        {
                            s.Type = DataType.I;
                        }
                        //output symbol
                        else if (s.Address.ToUpper().Contains("Q") || s.Address.ToUpper().Contains("A") && !s.Address.ToUpper().Contains("P") && !s.Address.ToUpper().Contains("D"))
                        {
                            s.Type = DataType.Q;
                        }
                        //mbit symbol
                        else if (s.Address.ToUpper().Contains("M") && !s.Address.ToUpper().Contains("P") && !s.Address.ToUpper().Contains("D"))
                        {
                            s.Type = DataType.M;
                        }



                        s.Comment = raw[3].Replace("\"", "");
                        Symbols.Add(s);
                    } while (!reader.EndOfStream);

                }
            }
        }
        public List<Symbol> FilterByAddress(string[] filters)
        {
            List<Symbol> keep = new List<Symbol>();
            try
            {
               
                foreach(var symbol in Symbols)
                {
                    foreach(var filter in filters)
                    {
                        if (symbol.Address.ToUpper().Contains(filter))
                        {
                            keep.Add(symbol);
                        }
                    }
                }
                
            }
            catch { System.Windows.Forms.MessageBox.Show("Error Filtering Symbols");}
            return keep;
        }
        public List<Symbol> FilterByType(DataType[] filters)
        {
            List<Symbol> keep = new List<Symbol>();
            try
            {
                foreach (var symbol in Symbols)
                {
                    foreach (var filter in filters)
                    {
                        if (symbol.Type.Equals(filter))
                        {
                            keep.Add(symbol);
                        }
                    }
                }
                
            }
            catch { System.Windows.Forms.MessageBox.Show("Error Filtering Symbols"); }
            return keep;
        }

        public List<Symbol> Symbols = new List<Symbol>();
    }
    [Serializable]
    public class Symbol
    {
        public string Name;
        public string Address;
        public string Comment;
        public string Description;
        public DataType Type;
        public bool State;
        public object Tag;
        public int Index;
        public int SubIndex;
    }
    [Serializable]
    public enum DataType
    {
        Bool, Word, UDT, DB, Timer, SFC, OB, FC, FB, Dword, Real, Counter, Byte, Other, None, M, I, E = 16, A, Q = 17
    }
    [Serializable]
    public class IOBuffer: Serializers.BinarySerializer
    {
        public IOBuffer Initialize(int bufferSize=16834)
        {
            InputBuffer = new byte[bufferSize];
            OutputBuffer = new byte[bufferSize];
            MbitBuffer = new byte[bufferSize];
            return this;
        }
        public byte[] InputBuffer;
        public string I_result = "";
        public string O_result="";
        public byte[] OutputBuffer;
        public byte[] MbitBuffer;
        public string M_result = "";
    }
}
