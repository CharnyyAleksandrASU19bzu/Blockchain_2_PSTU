using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Linq;

namespace Lab_02 {
    [Serializable()]
    class Block {
        public Block(string ts, string dat, string hs, int nc) {
            timestamp = ts;
            data = dat;
            hash = hs;
            nonce = nc;
        }

        string timestamp;
        string data;
        string hash;
        int nonce;

        public string GetTimestamp => timestamp;
        public string GetData => data;
        public string GetHash => hash;
        public int GetNonce => nonce;
        
        public string SetData {
            get => data;
            set => data = value;
        }
    }

    class Blockchain {
        static List<Block> blockchain = new List<Block>();
        static int difficulity = 2;
       
        public static List<Block> GetBlockchain => blockchain;

        #region Add

        public static void AddBlock(string dat) {
            string prvHash;
            int nonce = 0;
            string timestamp = Convert.ToString(DateTime.Now);

            if (blockchain.Count == 0) {
                prvHash = "";

                while (true) {
                    string newHash = getHash(timestamp, dat, prvHash, nonce);

                    if (newHash.StartsWith(String.Concat(Enumerable.Repeat("0", difficulity)))) {
                        Console.WriteLine("Найден блок {0}, nonce - {1}", newHash, nonce);

                        blockchain.Add(new Block(timestamp, dat, newHash, nonce));

                        break;
                    }
                    else nonce++;
                }
            }
            else {
                prvHash = blockchain.Last().GetHash;

                while (true) {
                    string newHash = getHash(timestamp, dat, prvHash, nonce);

                    if (newHash.StartsWith(String.Concat(Enumerable.Repeat("0", difficulity)))) {
                        Console.WriteLine("Найден блок {0}, nonce - {1}", newHash, nonce);

                        blockchain.Add(new Block(timestamp, dat, newHash, nonce));

                        break;
                    }
                    else nonce++;
                }
            }
        }

        #endregion

        #region Edit

        public static void EditBlock(int block, string data) {
            blockchain[block].SetData = data;
        }

        #endregion

        #region Show

        public static void Show() {
            int i = 0;

            foreach (Block blc in blockchain) {
                Console.WriteLine("{0}, {1}, {2}, {3}, {4}", i, blc.GetData, blc.GetHash, blc.GetTimestamp, blc.GetNonce);
                
                i++;
            }
        }

        #endregion

        #region Verification

        static string getHash(string ts, string dat, string prvHash, int nc) {
            using (SHA256 hash = SHA256Managed.Create()) {
                return String.Concat(hash
                    .ComputeHash(Encoding.UTF8.GetBytes(ts + dat + prvHash + nc))
                    .Select(item => item.ToString("x2"))); ;
            }
        }

        public static void Verification() {
            if (blockchain.Count != 0) {
                for (int i = 1; i != blockchain.Count; i++) {
                    if (getHash(blockchain[i].GetTimestamp,
                                blockchain[i].GetData,
                                blockchain[i - 1].GetHash,
                                blockchain[i].GetNonce) == blockchain[i].GetHash) Console.WriteLine("Block {0} - OK", i);
                    else {
                        Console.WriteLine("Найдено несоответствие !!!");

                        return;
                    }

                }

                Console.WriteLine("Все блоки подтверждены успешно!!");
            }
        }

        #endregion

        #region File

        static BinaryFormatter formatter = new BinaryFormatter();

        public static void SaveToFile(string filename) {
            using (FileStream fs = new FileStream(filename + ".amber", FileMode.Create)) {
                formatter.Serialize(fs, blockchain);
            }
        }

        public static void LoadFromFile(string filename) {
            using (FileStream fs = new FileStream(filename + ".amber", FileMode.OpenOrCreate)) {
                blockchain = (List<Block>)formatter.Deserialize(fs);

                foreach (Block blc in blockchain) {
                    Console.WriteLine("{0}, {1}, {2}, {3}", blc, blc.GetData, blc.GetHash, blc.GetTimestamp);
                }
            }
        }

        #endregion
    }
}
