using ImrPlatform.Constant;
using IMRUtils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ImrPlatform.Communication
{
    public class ComnService
    {
        private JirehService.JirehFrameworkClient serviceClient;

        public ComnService()
        {
            Init();
        }

        public void Init()
        {
            serviceClient = ConnectService(Properties.Settings.Default.EndPointAddress.ToString());
        }

        private JirehService.JirehFrameworkClient ConnectService(string sEndpointAddress)
        {
            BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
            basicHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Ntlm;
            basicHttpBinding.MaxReceivedMessageSize = 2147483647;
            basicHttpBinding.MaxBufferPoolSize = 2147483647;
            basicHttpBinding.ReceiveTimeout = TimeSpan.MaxValue;
            basicHttpBinding.OpenTimeout = TimeSpan.MaxValue;
            basicHttpBinding.SendTimeout = TimeSpan.MaxValue;
            basicHttpBinding.CloseTimeout = TimeSpan.MaxValue;
            basicHttpBinding.ReaderQuotas.MaxStringContentLength = 2147483647;
            basicHttpBinding.ReaderQuotas.MaxBytesPerRead = 2147483647;
            basicHttpBinding.ReaderQuotas.MaxArrayLength = 2147483647;

            EndpointAddress endpointAddress = new EndpointAddress(sEndpointAddress);

            JirehService.JirehFrameworkClient client = new JirehService.JirehFrameworkClient(basicHttpBinding, endpointAddress);

            return client;
        }

        /// <summary>
        /// 서비스 호출 함수
        /// </summary>
        public string CallService(string sServiceParams)
        {
            string sResult;
            try
            {
                sResult = (new IMRUtils.Encryption()).AESDecrypt256(serviceClient.CallService(sServiceParams));
            }
            catch (Exception ex)
            {
                sResult = "CallService Error : " + ex.Message;
            }

            return sResult;
        }

        /// <summary>
        /// 서비스 정보 객체 생성 함수
        /// </summary>
        /// <param name="sServiceName">Constant.Service.ServiceName 에서 선택</param>
        /// <param name="dicParams">Dictionary에 정리 한 파라미터</param>
        /// <returns></returns>
        public IMRUtils.UdtServiceInfo CreateServiceInfo(string sServiceName, Dictionary<string, string> dicParams)
        {
            IMRUtils.UdtServiceInfo udtServiceInfo = new IMRUtils.UdtServiceInfo();

            udtServiceInfo.ServiceName = sServiceName;
            udtServiceInfo.ConnectName = Constant.Service.ConnectName.TEST;
            udtServiceInfo.InputType = Constant.Service.Type.JSON;
            udtServiceInfo.ReturnType = Constant.Service.Type.XML;

            // 파라미터 있는지 체크
            if (null != dicParams || 0 < dicParams.Count)
            {
                udtServiceInfo.DicParams = dicParams;
            }

            return udtServiceInfo;
        }
    }
}
