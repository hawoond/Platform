using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ImrPlatform.Communication
{
    public class CallQuery
    {
        public Communication.ComnService Service;
        private IMRUtils.CreateParameter createParameter;
        private IMRUtils.Encryption encryption = new IMRUtils.Encryption();
        public CallQuery()
        {
            Service = new ComnService();
            createParameter = new IMRUtils.CreateParameter();
        }

        /// <summary>
        /// 쿼리호출
        /// </summary>
        /// <param name="udtServiceInfo"></param>
        /// <returns></returns>
        private DataSet CallParams(IMRUtils.UdtServiceInfo udtServiceInfo)
        {
            // XML(string) 형태의 결과 값 저장 변수
            string sResult = string.Empty;
            // DataSet 변환 후 결과 값 저장 변수
            DataSet dsResult;

            try
            {
                // 서비스 호출 정보 생성, 파라미터 암호화
                string sServiceParams = encryption.AESEncrypt256(createParameter.CreateParams(udtServiceInfo));
                // 서비스 호출
                sResult = Service.CallService(sServiceParams);
                // DataSet으로 변환
                dsResult = IMRUtils.TypeParser.XmlToDataSet(sResult);
            }
            catch (Exception ex)
            {
                // 통신 오류 전체 예외 체크
                // 예외 처리 종류 나누기 필요
                // ex) xml 파싱 오류, DataSet 변환 오류, TiemOut 체크

                dsResult = Common.StaticUtils.ExceptionDataSetResult(ex.Message);
            }

            // 결과 값 반환
            return dsResult;
        }

        public DataSet TEST()
        {
            // 서비스 정보 및 파라미터 담을 ServiceInfo 객체 생성
            IMRUtils.UdtServiceInfo udtServiceInfo = Service.CreateServiceInfo(Constant.Service.ServiceName.SELECT.S02, null);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////// 파라미터 있을 경우 예시                                                                                                                       /////
            ///////IMRUtils.UdtServiceInfo udtServiceInfo = Service.CreateServiceInfo(Constant.Service.ServiceName.SELECT.S02, new Dictionary<string, string>()   /////
            ///////{                                                                                                                                              /////
            ///////    { "키1","값1" }                                                                                                                            /////
            ///////    ,{ "키2","값2" }                                                                                                                           /////
            ///////});                                                                                                                                            /////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // 서비스 호출 및 결과 값 반환
            return CallParams(udtServiceInfo);
        }

        /// <summary>
        /// 사용자 로그인
        /// </summary>
        /// <param name="sId"></param>
        /// <param name="sPw"></param>
        /// <returns></returns>
        public DataSet S_CO_USR_LGN_SIN(string sId, string sPw)
        {
            IMRUtils.UdtServiceInfo udtServiceInfo = Service.CreateServiceInfo(Constant.Service.ServiceName.SELECT.CO_USR_LGN_SIN, new Dictionary<string, string>()
            {
                { "USER_NO",sId }
                ,{ "USER_PW",encryption.AESEncrypt256(sPw) }
            });

            // 서비스 호출 및 결과 값 반환
            return CallParams(udtServiceInfo);
        }

        /// <summary>
        /// 컨텐츠 메인 뷰 컨텐츠 정보 조회
        /// </summary>
        /// <param name="sConsId"></param>
        /// <returns></returns>
        public DataSet S_CV_CTS_INFO_SIN(string sConsId)
        {
            IMRUtils.UdtServiceInfo udtServiceInfo = Service.CreateServiceInfo(Constant.Service.ServiceName.SELECT.CV_CTS_INFO_SIN, new Dictionary<string, string>()
            {
                { "CONS_ID",sConsId }
            });

            // 서비스 호출 및 결과 값 반환
            return CallParams(udtServiceInfo);
        }

        /// <summary>
        /// 컨텐츠 메인 뷰 리뷰 리스트 조회
        /// </summary>
        /// <param name="sConsId"></param>
        /// <returns></returns>
        public DataSet S_CV_CTS_REV_LIST(string sConsId)
        {
            IMRUtils.UdtServiceInfo udtServiceInfo = Service.CreateServiceInfo(Constant.Service.ServiceName.SELECT.CV_CTS_REV_LIST, new Dictionary<string, string>()
            {
                { "CONS_ID",sConsId }
            });

            // 서비스 호출 및 결과 값 반환
            return CallParams(udtServiceInfo);
        }

        /// <summary>
        /// 리뷰 입력하기
        /// </summary>
        /// <param name="sUserNo">유저 아이디</param>
        /// <param name="sConsId">컨텐츠 아이디</param>
        /// <param name="sRevDoc">리뷰 내용</param>
        /// <param name="sConsSt">별점</param>
        /// <returns></returns>
        public DataSet I_MA_USR_REV_SIN(string sUserNo, string sConsId, string sRevDoc, string sConsSt)
        {
            IMRUtils.UdtServiceInfo udtServiceInfo = Service.CreateServiceInfo(Constant.Service.ServiceName.INSERT.MA_USR_REV_SIN, new Dictionary<string, string>()
            {
                { "USER_NO",sUserNo },
                { "CONS_ID",sConsId },
                { "REV_DOC",sRevDoc },
                { "CONS_ST",sConsSt }
            });

            // 서비스 호출 및 결과 값 반환
            return CallParams(udtServiceInfo);
        }

        /// <summary>
        /// 메인뷰 컨텐츠 리스트 조회
        /// </summary>
        /// <param name="sPageNo">페이지 번호</param>
        /// <param name="sSearchWord">검색 문자열</param>
        /// <param name="sCategory">검색 카테고리</param>
        /// <returns></returns>
        public DataSet S_MA_CTS_SRCH_LIST(string sPageNo, string sSearchWord, string sCategory)
        {
            IMRUtils.UdtServiceInfo udtServiceInfo = Service.CreateServiceInfo(Constant.Service.ServiceName.SELECT.MA_CTS_SRCH_LIST, new Dictionary<string, string>()
            {
                { "PAGE_NO",sPageNo },
                { "SEARCH_WORD",sSearchWord },
                { "CATEGORY",sCategory }
            });

            // 서비스 호출 및 결과 값 반환
            return CallParams(udtServiceInfo);
        }

        /// <summary>
        /// 찜목록 리스트 조회
        /// </summary>
        /// <param name="sPageNo">페이지 번호</param>
        /// <param name="sSearchWord">검색 문자열</param>
        /// <param name="sCategory">검색 카테고리</param>
        /// <returns></returns>
        public DataSet S_MA_USR_FAV_LIST(string sUserNo)
        {
            IMRUtils.UdtServiceInfo udtServiceInfo = Service.CreateServiceInfo(Constant.Service.ServiceName.SELECT.MA_USR_FAV_LIST, new Dictionary<string, string>()
            {
                { "USER_NO",sUserNo },
            });

            // 서비스 호출 및 결과 값 반환
            return CallParams(udtServiceInfo);
        }

        /// <summary>
        /// 사용자 구매 내역 조회
        /// </summary>
        /// <param name="sUserNo"></param>
        /// <returns></returns>
        public DataSet S_MA_USR_PRCH_LIST(string sUserNo)
        {
            IMRUtils.UdtServiceInfo udtServiceInfo = Service.CreateServiceInfo(Constant.Service.ServiceName.SELECT.MA_USR_PRCH_LIST, new Dictionary<string, string>()
            {
                { "USER_NO",sUserNo },
            });

            // 서비스 호출 및 결과 값 반환
            return CallParams(udtServiceInfo);
        }

        /// <summary>
        /// 사용자 리뷰 리스트 조회
        /// </summary>
        /// <param name="sUserNo"></param>
        /// <returns></returns>
        public DataSet S_MA_USR_REV_LIST(string sUserNo)
        {
            IMRUtils.UdtServiceInfo udtServiceInfo = Service.CreateServiceInfo(Constant.Service.ServiceName.SELECT.MA_USR_REV_LIST, new Dictionary<string, string>()
            {
                { "USER_NO",sUserNo },
            });

            // 서비스 호출 및 결과 값 반환
            return CallParams(udtServiceInfo);
        }

        /// <summary>
        /// 찜 목록 데이터 등록
        /// </summary>
        /// <param name="sUserNo">유저 아이디</param>
        /// <returns></returns>
        public DataSet I_MA_USR_FAV_SIN(string sUserNo, string sConsId)
        {
            IMRUtils.UdtServiceInfo udtServiceInfo = Service.CreateServiceInfo(Constant.Service.ServiceName.INSERT.MA_USR_FAV_SIN, new Dictionary<string, string>()
            {
                { "USER_NO",sUserNo },
                { "CONS_ID",sConsId }
            });

            // 서비스 호출 및 결과 값 반환
            return CallParams(udtServiceInfo);
        }

        /// <summary>
        /// 컨텐츠 구매 리스트 추가
        /// </summary>
        /// <param name="sUserNo"></param>
        /// <returns></returns>
        public DataSet I_MA_USR_PRCH_SIN(string sUserNo, string sConsId)
        {
            IMRUtils.UdtServiceInfo udtServiceInfo = Service.CreateServiceInfo(Constant.Service.ServiceName.INSERT.MA_USR_PRCH_SIN, new Dictionary<string, string>()
            {
                { "USER_NO",sUserNo },
                { "CONS_ID",sConsId }
            });

            // 서비스 호출 및 결과 값 반환
            return CallParams(udtServiceInfo);
        }

        /// <summary>
        /// 리뷰 데이터 삭제(업데이트)
        /// </summary>
        /// <param name="sUserNo"></param>
        /// <returns></returns>
        public DataSet U_MA_USR_REV_SIN(string sUserNo, string sConsSeq ,string sConsId)
        {
            IMRUtils.UdtServiceInfo udtServiceInfo = Service.CreateServiceInfo(Constant.Service.ServiceName.UPDATE.MA_USR_REV_SIN, new Dictionary<string, string>()
            {
                { "USER_NO",sUserNo },
                { "CONS_SEQ",sConsSeq },
                { "CONS_ID",sConsId }
            });

            // 서비스 호출 및 결과 값 반환
            return CallParams(udtServiceInfo);
        }

        /// <summary>
        /// 찜 목록 데이터 삭제
        /// </summary>
        /// <param name="sUserNo"></param>
        /// <returns></returns>
        public DataSet D_MA_USR_FAV_SIN(string sUserNo, string sConsId)
        {
            IMRUtils.UdtServiceInfo udtServiceInfo = Service.CreateServiceInfo(Constant.Service.ServiceName.DELETE.MA_USR_FAV_SIN, new Dictionary<string, string>()
            {
                { "CONS_ID",sConsId },
                { "USER_NO",sUserNo }
            });

            // 서비스 호출 및 결과 값 반환
            return CallParams(udtServiceInfo);
        }
        
    }
}
