//만든이: 송용단
//직급: 수습사원
//작성일: 2015.10.15
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
    class MainApp
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\u_ex150907.log");
            string pattern = @"[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}";
            List<string> ipValues = new List<string>(); // 추출한 IP들
            int dataCount = 0; // 글자형에 맞는 IP의 개수
            int sameCount = 0; // 같은 IP의 개수
            List<string> singleIps = new List<string>(); // 중복되지 않는 IP들
            List<string> ipDatas = new List<string>(); // IP에 대한 데이터들 - 짝수행 : IP 값, 홀수행 : 중복된 IP의 개수

            foreach (string line in lines)
            {
                 Match m = Regex.Match(line, pattern);

                while (m.Success)
                {
                    ipValues.Add(m.Value);
                
                    if(dataCount % 2 > 0)
                    {
                        ipValues.Remove(m.Value);
                    }

                    dataCount++;

                    m = m.NextMatch();

                }
            }

            // 중복되지 않는 IP들을 구한다.
            singleIps = ipValues.Distinct().ToList();

            // 배열의 개수만큼 반복한다.
            for (int i = 0; i < singleIps.Count; i++)
            {
                // 배열의 첫 번째 값과 같은 IP의 개수를 센다.
                ipDatas.Add(singleIps[i]);
                for(int j = 0; j < ipValues.Count; j++)
                {
                    if(ipValues[j].Equals(singleIps[i]))
                    {
                        sameCount++;
                    }
                }
                // 자기 자신의 개수로 하나를 더한다.
                sameCount++;
                // 중복 개수를 추가한다.
                ipDatas.Add(sameCount.ToString());
                // 중복 개수를 초기화한다.
                sameCount = 0;
            }

            for(int i = 0; i < ipDatas.Count; i++)
            {
                if(i % 2 == 0)
                { 
                    Console.Write("{0}", ipDatas[i]);
                }
                else
                {
                    Console.Write(" {0}개\n", ipDatas[i]);
                }
            }

            Console.ReadKey();
        }
    }
}
