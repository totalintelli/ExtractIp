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
            string input =
                "2015-09-0715:36:20 115.68.15.124 GET /tfs/web - 80 - 115.68.15.124Mozilla/5.0+(Windows+NT+6.1;+WOW64;+Trident/7.0;+rv:11.0)+like+Gecko 403 6 51794" +
                "2015-09-0715:36:33 115.68.15.124 GET /tfs/web - 80 - 221.139.32.253Mozilla/5.0+(Windows+NT+6.2;+WOW64)+AppleWebKit/537.36+(KHTML,+like+Gecko)+Chrome"
                + "/45.0.2454.85+Safari/537.36403 6 5 140" +
                "2015-09-0715:36:33 115.68.15.124 GET /favicon.ico - 80 - 221.139.32.253Mozilla/5.0+(Windows+NT+6.2;+WOW64)+AppleWebKit/537.36+(KHTML,+like+Gecko)+"
                + "Chrome/45.0.2454.85+Safari/537.36403 6 5 78"
                + "2015-09-07 15:36:54 127.0.0.1 GET /tfs/web - 8080 - 127.0.0.1 Mozilla/5.0+(Windows+NT+6.1;+WOW64;+Trident/7.0;+rv:11.0)+like+Gecko 401 2 5 0"
                + "2015-09-07 15:36:59 127.0.0.1 GET /tfs/web - 8080 AEROBIC1004\\wharf 127.0.0.1 Mozilla/5.0+(Windows+NT+6.1;+WOW64;+Trident/7.0;+rv:11.0)+like+Gecko 301 0 0 0"
            +"2015-09-07 15:37:03 fe80::5d69:3b9:2b7a:d8e2%13 POST /tfs/TeamFoundation/Administration/v3.0/LocationService.asmx - 8080 - fe80::5d69:3b9:2b7a:d8e2%13 Team+Foundation+10.0.40219.457,+(WebAccess+10.0.0) 403 6 5 15"
            + "2015-09-07 15:37:03 fe80::5d69:3b9:2b7a:d8e2%13 POST /tfs/Services/v1.0/Registration.asmx - 8080 - fe80::5d69:3b9:2b7a:d8e2%13 Team+Foundation+10.0.40219.457,+(WebAccess+10.0.0) 403 6 5 0"
            + "2015-09-07 15:37:03 fe80::5d69:3b9:2b7a:d8e2%13 POST /tfs/Services/v1.0/Registration.asmx - 8080 - fe80::5d69:3b9:2b7a:d8e2%13 Team+Foundation+10.0.40219.457,+(WebAccess+10.0.0) 403 6 5 0";

            string pattern = @"[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}";

            Match m = Regex.Match(input, pattern);

            List<string> ipValues = new List<string>(); // 추출한 IP들

            int dataCount = 0; // 글자형에 맞는 IP의 개수
            int sameCount = 0; // 같은 IP의 개수

            List<string> ipDatas = new List<string>(); // IP에 대한 데이터들 - 짝수행 : IP 값, 홀수행 : 중복된 IP의 개수

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

            // 배열의 개수만큼 반복한다.
            for (int i = 0; i < ipValues.Count; i++)
            {
                // 배열의 첫 번째 값과 같은 IP의 개수를 센다.
                ipDatas.Add(ipValues[i]);
                for(int j = 0; j < ipValues.Count; j++)
                {
                    if(ipValues[j] == ipValues[i])
                    {
                        sameCount++;
                        // 같은 IP들을 찾아서 없앤다.
                        ipValues.Remove(ipValues[j]);
                    }
                }
                sameCount++;
                ipDatas.Add(sameCount.ToString());
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
