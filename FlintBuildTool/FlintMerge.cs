using System;
using System.Collections.Generic;
using System.IO;

namespace FlintBuildTool {
    internal static class FlintMerge {
        private class FirmwareBinary {
            public FirmwareBinary(int offset, byte[] data) {
                Offset = offset;
                Data = data;
            }

            public int Offset {
                get;
            }

            public byte[] Data {
                get;
            }
        }

        public static void MergeBinary(string[] args) {
            List<FirmwareBinary> binarys = new List<FirmwareBinary>();
            string outFileName = "MergedBinary.bin";
            int index = 0;
            int totalSize = 0;
            while(index < args.Length) {
                if(args[index] == "-o") {
                    index++;
                    outFileName = args[index++];
                }
                else {
                    int offset = Convert.ToInt32(args[index++], 16);
                    string filePath = args[index++];
                    byte[] data;
                    if(File.Exists(filePath))
                        data = File.ReadAllBytes(filePath);
                    else
                        throw new FileNotFoundException("", filePath);
                    FirmwareBinary binary = new FirmwareBinary(offset, data);
                    int endOfAddr = binary.Offset + binary.Data.Length;
                    if(totalSize < endOfAddr)
                        totalSize = endOfAddr;
                    binarys.Add(binary);
                }
            }

            byte[] mergeBinary = new byte[totalSize];
            for(int i = 0; i < mergeBinary.Length; i++)
                mergeBinary[i] = 0xFF;
            for(int i = 0; i < binarys.Count; i++)
                Array.Copy(binarys[i].Data, 0, mergeBinary, binarys[i].Offset, binarys[i].Data.Length);
            File.WriteAllBytes(outFileName, mergeBinary);
            Console.WriteLine("Created " + outFileName);
        }
    }
}
