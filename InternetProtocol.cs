//만든이: 송용단
//직급: 수습사원
//작성일: 2016.03.08
//목적: 로그 파일에서 IP만 추출
//특이 사항: 없음
//버전 1

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExtractIP
{
    class InternetProtocal
    {
        static void Main(string[] args)
        {
            InternetProtocal ip = new InternetProtocal();
            string[] lines;
            List<string> ipDatas;

            lines = ip.Load();
            ip.ExtractIp(lines, out ipDatas);
            ip.Output(ipDatas);
        }



        /*
        함수 이름 : ExtractIp
        기    능 : IP를 추출하고 추출한 Ip들의 개수를 센다.
        입    력 : 한 줄 로그들
        출    력 : IP 데이터들 
        */
         void ExtractIp(string[] lines, out List<string> ipDatas)
        {
            string pattern = @"[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}";
            List<string> tmpIpValues = new List<string>(); // IP 형식에 맞는 IP들
            List<string> ipValues = new List<string>(); // IP 값들
            int dataCount = 0; // 글자형에 맞는 IP의 개수
            int sameCount = 0; // 같은 IP의 개수
            List<string> singleIps = new List<string>(); // IP 목록
            ipDatas = new List<string>(); // IP에 대한 데이터들 - 짝수행 : IP 값, 홀수행 : 중복된 IP의 개수
            int k = 0; // IP 형식에 맞는 IP들에서의 위치

            foreach (string line in lines)
            {
                Match m = Regex.Match(line, pattern);

                while (m.Success)
                {
                    tmpIpValues.Add(m.Value);

                    dataCount++;

                    m = m.NextMatch();

                }
            }
                        
            while(k < tmpIpValues.Count)
            {
                if(k % 2 == 0)
                {
                    ipValues.Add(tmpIpValues[k]);
                }

                k++;
            }


            // IP 목록을 구한다.
            singleIps = ipValues.Distinct().ToList();

            // 배열의 개수만큼 반복한다.
            for (int i = 0; i < singleIps.Count; i++)
            {
                // IP에 대한 데이터들을 구한다.
                ipDatas.Add(singleIps[i]);
                // 배열의 첫 번째 값과 같은 IP의 개수를 센다.
                for (int j = 0; j < ipValues.Count; j++)
                {
                    if (ipValues[j].Equals(singleIps[i]))
                    {
                        sameCount++;
                    }
                }
                // 자기 자신의 개수로 하나를 더한다.
                sameCount++;
                // 중복 개수를 추가한다.
                ipDatas.Add(sameCount.ToString() + "개");
                // 중복 개수를 초기화한다.
                sameCount = 0;
            }
        }



        /*
        함수 이름 : Load
        기    능 : 파일을 읽어서 한 줄 로그들을 만든다.
        입    력 : 없음
        출    력 : 한 줄 로그들
        */
        string[] Load()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\u_ex150907.log");
            return lines;
        }



        /*
        함수 이름 : Output
        기    능 : IP 추출 결과를 콘솔에 출력한다.
        입    력 : IP 데이터들
        출    력 : 없음
        */
        void Output(List<string> ipDatas)
        {
            for (int i = 0; i < ipDatas.Count; i++)
            {
              Console.WriteLine("{0}", ipDatas[i]);
            }

            Console.ReadKey();
        }
    }
}
