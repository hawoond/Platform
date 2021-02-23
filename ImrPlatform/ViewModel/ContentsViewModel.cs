using EntityConnecter.Models.DB.SELECT;
using ImrPlatform.Common;
using ImrPlatform.Communication;
using ImrPlatform.Model;
using IMRUtils.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static IMRUtils.OverrideType;

namespace ImrPlatform.ViewModel
{
    public class ContentsViewModel : IMRUtils.WPF.MomViewModel
    {
        //private delegate void CSafeSetText(string text);
        //private delegate void CSafeSetMaximum(Int32 value);
        //private delegate void CSafeSetValue(Int32 value);

        //private CSafeSetText csst;
        //private CSafeSetMaximum cssm;
        //private CSafeSetValue cssv;
        // 다운로드 웹클라이언트
        private WebClient wcDownload;
        //public static Communication.ComnService Service;
        //private IMRUtils.CreateParameter createParameter = new IMRUtils.CreateParameter();

        private CallQuery callQuery;
        private S_CV_CTS_INFO_SIN CV_CTS_INFO_SIN;
        private S_CV_CTS_REV_LIST CV_CTS_REV_LIST;

        #region 생성 및 초기화

        /// <summary>
        /// 싱글턴 객체
        /// </summary>
        private static ContentsViewModel instance;
        public static ContentsViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ContentsViewModel();
                }
                return instance;
            }
        }

        /// <summary>
        /// 컨텐츠 아이디 세팅 함수
        /// 필요 데이터 통신도 수행
        /// </summary>
        /// <param name="sConsId"></param>
        public void GetConsId(string sConsId)
        {
            GetContentsData(sConsId);
            GetReviewList(sConsId);
        }

        /// <summary>
        /// 컨텐츠 정보 조회
        /// </summary>
        /// <param name="sConsId">컨텐츠 아이디</param>
        public void GetContentsData(string sConsId)
        {
            DataSet dsResult = new DataSet();

            dsResult = callQuery.S_CV_CTS_INFO_SIN(sConsId);

            if (0 == dsResult.Tables.Count)
            {
                return;
            }

            CV_CTS_INFO_SIN = (S_CV_CTS_INFO_SIN)StaticUtils.DataRowToClass(new S_CV_CTS_INFO_SIN(), dsResult.Tables[0].Rows[EnumDef.SERVICE.SINGLE_ROW]);

            ContentsTitle = CV_CTS_INFO_SIN.CONS_NM;
            ContentsDocument = CV_CTS_INFO_SIN.CONS_DOC;
            ContentsID = CV_CTS_INFO_SIN.CONS_ID;

            string sPrevPath = StaticUtils.DeCompressFolder(Convert.FromBase64String(CV_CTS_INFO_SIN.CONS_PREV_PATH));
            string[] arrPrevImg = Directory.GetFiles(sPrevPath);

            //배경 그래픽은 프리뷰이미지 0번째
            ContentsBackgroundImageUri = sPrevPath + @"\" + Path.GetFileName(arrPrevImg[0]);

            //썸네일 이미지 넣는법
            //System.Drawing.Image testImg = IMRUtils.TypeParser.ByteArrayToImage(Convert.FromBase64String(CV_CTS_INFO_SIN.CONS_THUMB_PATH));
            //mContentsImageUri = StaticUtils.ImageToImageSource(testImg);

            carouselData = new List<string>();
            for (int i = 0; i < arrPrevImg.Length; i++)
            {
                carouselData.Add(sPrevPath + @"\" + Path.GetFileName(arrPrevImg[i]));
            }
            CurrentCarouselData = sPrevPath + @"\" + Path.GetFileName(arrPrevImg[1]);

            //mContentsImageUri = CV_CTS_INFO_SIN.CONS_THUMB_PATH;
            ContentsPrice = CV_CTS_INFO_SIN.CONS_PRICE;
            InfoCopyDoc = CV_CTS_INFO_SIN.COPY_DOC;
            InfoFileSizeTitle = "대략적인 크기";
            InfoFileSizeDoc = CV_CTS_INFO_SIN.CONS_SIZE;
            ContentsStarCount = CV_CTS_INFO_SIN.STAR_RATE;
        }

        /// <summary>
        /// 리뷰 리스트 조회
        /// </summary>
        /// <param name="sConsId">컨텐츠 아이디</param>
        public void GetReviewList(string sConsId)
        {
            DataSet dsResult = new DataSet();

            dsResult = callQuery.S_CV_CTS_REV_LIST(sConsId);

            if (0 == dsResult.Tables.Count)
            {
                return;
            }
            CV_CTS_REV_LIST = (S_CV_CTS_REV_LIST)StaticUtils.DataRowToClass(new S_CV_CTS_REV_LIST(), dsResult.Tables[0].Rows[EnumDef.SERVICE.SINGLE_ROW]);

            ReviewList = new MTObservableCollection<UdtReviewItem>();
            UdtReviewItem udtReviewItem = new UdtReviewItem();

            if (0 != CV_CTS_REV_LIST.USER_NM.Length)
            {
                udtReviewItem.UserName = CV_CTS_REV_LIST.USER_NM;
            }
            else
            {
                if (0 != CV_CTS_REV_LIST.USER_NICK.Length)
                {
                    udtReviewItem.UserName = CV_CTS_REV_LIST.USER_NICK;
                }
                else
                {
                    udtReviewItem.UserName = CV_CTS_REV_LIST.USER_NO;
                }
            }
            udtReviewItem.ReviewDate = CV_CTS_REV_LIST.REV_DTM;//.ToString("yyyy/MM/dd HH:mm");//DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            udtReviewItem.ReviewDoc = CV_CTS_REV_LIST.REV_DOC;
            udtReviewItem.ReviewStarCount = CV_CTS_REV_LIST.STAR_RATE;
            ReviewList.Add(udtReviewItem);

        }

        /// <summary>
        /// 생성자
        /// </summary>
        public ContentsViewModel()
        {
            // 서비스 통신 객체 null 체크
            if (null == callQuery)
            {
                // 서비스 통신 객체 생성
                callQuery = new CallQuery();
            }
            Init();
        }

        private void Init()
        {
            /////////////////////////////////데모 데이터 세팅/////////////////////////////////////////////

            ///컨텐츠 설명///
            //ContentsTitle = Properties.Resources.DEMO_CONTENT_TITLE;
            //ContentsDocument = Properties.Resources.DEMO_CONTENT_DESC;
            //ContentsBackgroundImageUri = @"/ImrPlatform;component/Image/test.jpg";
            //mContentsImageUri = @"/ImrPlatform;component/Image/test.jpg";
            //ContentsPrice = Properties.Resources.DEMO_CONTENT_PRICE;
            //InfoCopyDoc = "Copyright Whitewater Foundry, Ltd. Co.";
            //InfoFileSizeTitle = "대략적인 크기";
            //InfoFileSizeDoc = "1854.6TB";
            //ContentsStarCount = "5";

            ///리뷰///
            //ReviewList = new MTObservableCollection<UdtReviewItem>();
            //UdtReviewItem udtReviewItem = new UdtReviewItem();

            //udtReviewItem.UserName = "조규남";
            //udtReviewItem.ReviewDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            //udtReviewItem.ReviewDoc = "최고의 걸작. 아직도 가슴 속에서 쉽게 잊혀지지 않는군요.. 우리나라의 현시국을 적나라하게 보여주는 과감하면서도 화려한 검정아이콘. 개발자의 혼신의 노력이 돋보여지는 고퀄의 3초 그래픽. 이런 최고의 걸작을 이 용량에 만나볼수 있다는게 너무나도 감사하고 영광입니다 아직도 이 앱을 생각하면 눈물이 코로 가는줄도 모르겠습니다.. 개발자님에게 감사의 말씀을 전하고 싶습니다.";
            //udtReviewItem.ReviewStarCount = "5";
            //ReviewList.Add(udtReviewItem);

            /////////////////////////////////진짜 세팅, 가데이터 아님//////////////////////////////////////
            InfoCopyTitle = Properties.Resources.STRING_COPY_WRITE;

            //DecrementCommand = new SimpleCommand<Object, Object>(ExecuteDecrementCommand);
            //IncrementCommand = new SimpleCommand<Object, Object>(ExecuteIncrementCommand);

            // 대리자 초기화
            //csst = new CSafeSetText(CrossSafeSetTextMethod);
            //cssm = new CSafeSetMaximum(CrossSafeSetMaximumMethod);
            //cssv = new CSafeSetValue(CrossSafeSetValueMethod);

            // 웹 클라이언트 개체를 초기화
            wcDownload = new WebClient();

            // 이벤트 연결
            wcDownload.DownloadFileCompleted += new AsyncCompletedEventHandler(fileDownloadCompleted);
            wcDownload.DownloadProgressChanged += new DownloadProgressChangedEventHandler(fileDownloadProgressChanged);
        }

        private MTObservableCollection<UdtReviewItem> mReviewList;
        public MTObservableCollection<UdtReviewItem> ReviewList
        {
            get
            {
                return mReviewList ?? (mReviewList = new MTObservableCollection<UdtReviewItem>());
            }
            set
            {
                mReviewList = value;
            }
        }

        #endregion

        //#region OnPropertyChanged 구현
        ///// <summary>
        ///// PropertyChanged 이벤트 인터페이스
        ///// </summary>
        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void OnPropertyChanged(string name)
        //{
        //    PropertyChangedEventHandler handler = PropertyChanged;
        //    if (handler != null)
        //    {
        //        handler(this, new PropertyChangedEventArgs(name));
        //    }
        //}
        //#endregion

        #region 바인딩 객체

        #region 내부 전역 변수
        private string mContentsBackgroundImageUri;
        private System.Windows.Media.ImageSource mContentsImageUri;
        private string mContentsTitle;
        private string mContentsStarCount;
        private string mContentsDocument;
        private string mContentsPrice;
        private string mInfoCopyTitle;
        private string mInfoCopyDoc;
        private string mInfoFileSizeTitle;
        private string mInfoFileSizeDoc;
        private string mContentsID;
        private string currentCarouselData;
        private int currentCarouselPos = 0;
        private int mDownloadPersent;
        private List<String> carouselData;
        private string mReviewInputDoc;
        #endregion

        #region 바인딩 적용 변수
        /// <summary>
        /// 타이틀 배경 이미지 주소
        /// </summary>
        public string ContentsBackgroundImageUri
        {
            get
            {
                return mContentsBackgroundImageUri;
            }
            set
            {
                mContentsBackgroundImageUri = value.ToString();
                this.OnPropertyChanged("ContentsBackgroundImageUri");
            }
        }

        /// <summary>
        /// 컨텐츠 이미지 주소
        /// </summary>
        public System.Windows.Media.ImageSource ContentsImageUri
        {
            get
            {
                return mContentsImageUri;
            }
            set
            {
                mContentsImageUri = value;
                this.OnPropertyChanged("ContentsImageUri");
            }
        }

        /// <summary>
        /// 컨텐츠 타이틀 이름
        /// </summary>
        public string ContentsTitle
        {
            get
            {
                return mContentsTitle;
            }
            set
            {
                mContentsTitle = value.ToString();
                this.OnPropertyChanged("ContentsTitle");
            }
        }

        /// <summary>
        /// 별 찍기
        /// </summary>
        public string ContentsStarCount
        {
            get
            {
                return mContentsStarCount;
            }
            set
            {
                mContentsStarCount = string.Empty;
                int nCount = 0;
                try
                {
                    nCount = int.Parse(value);
                }
                catch
                {
                    nCount = 0;
                }
                finally
                {
                    for (int i = 0; i < nCount; i++)
                    {
                        mContentsStarCount += "★";
                    }
                }
                this.OnPropertyChanged("ContentsStarCount");

            }
        }

        /// <summary>
        /// 컨텐츠 설명
        /// </summary>
        public string ContentsDocument
        {
            get
            {
                return mContentsDocument;
            }
            set
            {
                mContentsDocument = value.ToString();
                this.OnPropertyChanged("ContentsDocument");
            }
        }

        /// <summary>
        /// 컨텐츠 가격
        /// </summary>
        public string ContentsPrice
        {
            get
            {
                return mContentsPrice;
            }
            set
            {
                if ("0".Equals(value.ToString()))
                {
                    mContentsPrice = "무료";
                }
                else
                {
                    mContentsPrice = value.ToString() + Properties.Resources.MONETARY_UNIT;
                }
                this.OnPropertyChanged("ContentsPrice");
            }
        }

        /// <summary>
        /// 저작권
        /// </summary>
        public string InfoCopyTitle
        {
            get
            {
                return mInfoCopyTitle;
            }
            set
            {
                mInfoCopyTitle = value.ToString();

                this.OnPropertyChanged("InfoCopyTitle");
            }
        }

        /// <summary>
        /// 저작권 정보
        /// </summary>
        public string InfoCopyDoc
        {
            get
            {
                return mInfoCopyDoc;
            }
            set
            {
                mInfoCopyDoc = value.ToString();

                this.OnPropertyChanged("InfoCopyDoc");
            }
        }

        /// <summary>
        /// 파일 사이즈
        /// </summary>
        public string InfoFileSizeTitle
        {
            get
            {
                return mInfoFileSizeTitle;
            }
            set
            {
                mInfoFileSizeTitle = value.ToString();

                this.OnPropertyChanged("InfoFileSizeTitle");
            }
        }

        /// <summary>
        /// 파일 사이즈 내용
        /// </summary>
        public string InfoFileSizeDoc
        {
            get
            {
                return mInfoFileSizeDoc;
            }
            set
            {
                mInfoFileSizeDoc = value.ToString();

                this.OnPropertyChanged("InfoFileSizeDoc");
            }
        }

        /// <summary>
        /// 컨텐츠 아이디
        /// </summary>
        public string ContentsID
        {
            get
            {
                return mContentsID;
            }
            set
            {
                mContentsID = value.ToString();
                this.OnPropertyChanged("ContentsID");
            }
        }

        /// <summary>
        /// 이미지 캐러셀 리스트
        /// </summary>
        public List<String> CarouselData
        {
            get { return carouselData; }
        }

        /// <summary>
        /// 이미지 캐러셀 현재 선택된 뇨속
        /// </summary>
        public string CurrentCarouselData
        {
            get { return currentCarouselData; }
            set
            {
                if (currentCarouselData != value)
                {
                    currentCarouselData = value;
                    currentCarouselPos = carouselData.IndexOf(currentCarouselData);
                    this.OnPropertyChanged("CurrentCarouselData");
                }
            }
        }

        /// <summary>
        /// 프로그래스바 퍼센트 수치 Value
        /// </summary>
        public int DownloadPersent
        {
            get
            {
                return mDownloadPersent;
            }
            set
            {
                mDownloadPersent = value;
                this.OnPropertyChanged("DownloadPersent");
            }
        }

        /// <summary>
        /// 입력 받은 리뷰 문자열 값
        /// </summary>
        public string ReviewInputDoc
        {
            get
            {
                return mReviewInputDoc;
            }
            set
            {
                mReviewInputDoc = value;
                this.OnPropertyChanged("ReviewInputDoc");
            }
        }
        
        #endregion

        #region 커맨드 변수

        public ICommand DecrementCommand { get; private set; }
        public ICommand IncrementCommand { get; private set; }
        /// <summary>
        /// 리뷰 등록
        /// </summary>
        private ICommand IReviewCommand;

        /// <summary>
        /// 컨텐츠 뷰 종료
        /// </summary>
        private ICommand IExitCommand;

        /// <summary>
        /// 컨텐츠 구매
        /// </summary>
        private ICommand IBuyContentsCommand;
        #endregion
        #region 커맨드

        /// <summary>
        /// 리뷰 등록 커맨드
        /// </summary>
        public ICommand ReviewCommand
        {
            get
            {
                if (IReviewCommand == null)
                {
                    IReviewCommand = new DelegateCommand(RegistReview);
                }
                return IReviewCommand;
            }
        }

        /// <summary>
        /// 컨텐츠 뷰 종료 커맨드
        /// </summary>
        public ICommand ExitCommand
        {
            get
            {
                if (IExitCommand == null)
                {
                    IExitCommand = new DelegateCommand(ExitView);
                }
                return IExitCommand;
            }
        }

        /// <summary>
        /// 컨텐츠 다운로드/구매 커맨드
        /// </summary>
        public ICommand BuyContentsCommand
        {
            get
            {
                if (IBuyContentsCommand == null)
                {
                    IBuyContentsCommand = new DelegateCommand(BuyContents);
                }
                return IBuyContentsCommand;
            }
        }

        #endregion

        #region 커맨드 함수

        /// <summary>
        /// 리뷰 등록 커맨드 함수
        /// </summary>
        /// <param name="obj"></param>
        private void RegistReview(object obj)
        {
            DataSet dsResult = new DataSet();

            dsResult = callQuery.I_MA_USR_REV_SIN(MainViewModel.LoginUser.USER_NO, CV_CTS_INFO_SIN.CONS_ID,ReviewInputDoc,"5");

            GetConsId(CV_CTS_INFO_SIN.CONS_ID);
        }

        /// <summary>
        /// 컨텐츠 팝업창 종료
        /// </summary>
        /// <param name="obj"></param>
        private void ExitView(object obj)
        {
            carouselData.Clear();
            ContentsBackgroundImageUri = string.Empty;
            ((Window)obj).Close();
        }

        /// <summary>
        /// 컨텐츠 구매 커맨드 함수
        /// </summary>
        /// <param name="obj"></param>
        private void BuyContents(object obj)
        {
            if (null == MainViewModel.LoginUser)
            {
                Common.StaticUtils.ShowLoginWindow();
                return;
            }
            // 커맨드 등록으로 구매 후 true 떨어지면 다운로드 시작
            bool isAuth = true;
            string sDesFilePath = @"D:\Contents\" + CV_CTS_INFO_SIN.CONS_PATH.Split(new string[] { @"\" }, StringSplitOptions.None)[3];
            string sDownloadUri = @"http://192.168.0.10:8027/SortieFolder/Platform/" + CV_CTS_INFO_SIN.CONS_TP + @"\" + CV_CTS_INFO_SIN.CONS_PATH.Split(new string[] { @"\" }, StringSplitOptions.None)[3];

            //WebClient wc = new WebClient();
            wcDownload.DownloadFileAsync(new Uri(@"http://192.168.0.10:8027/SortieFolder/Platform/" + CV_CTS_INFO_SIN.CONS_TP + @"\" + CV_CTS_INFO_SIN.CONS_PATH.Split(new string[] { @"\" }, StringSplitOptions.None)[3]), sDesFilePath);



            //// 커맨드 등록으로 구매 후 true 떨어지면 다운로드 시작
            //bool isAuth = true;

            //// 다운로드 시작(FTP)

            //JirehService.JirehFrameworkClient jirehService = new JirehService.JirehFrameworkClient();

            ////string sFileName = "Watch in 360 the inside of a nuclear reactor from the size of an atom with virtual reality.mkv";
            ////CV_CTS_INFO_SIN.

            ////FileStream fsResult = (FileStream)IMRUtils.TypeParser.StringToStream((new IMRUtils.Encryption()).AESDecrypt256(jirehService.FileDownload("192.168.0.10", "21", "wjkim", "rladndwo3", "Platform/Movie/Watch in 360 the inside of a nuclear reactor from the size of an atom with virtual reality.mkv")));

            ////byte[] buffer = new byte[1024]; // Change this to whatever you need
            ////MemoryStream fsResult = (MemoryStream)IMRUtils.TypeParser.StringToStream(jirehService.HttpDownload("Movie","test"));

            ////MemoryStream fsResult = (MemoryStream)IMRUtils.TypeParser.StringToStream(jirehService.HttpDownload(@"Movie\01.png"));

            //// 스트림을 바이트어레이로 변환 하고 DeCompressFolder 함수에 넣어서 저장한다 DeCompressFolder는 저장경로가 하드코딩되어 있어서 변경 해야됨 하드하드하드
            ////string sPrevPath = StaticUtils.DeCompressFolder(fsResult.ToArray(),"TempContents", @"D:\Contents");
            //System.GC.Collect();
            //System.GC.WaitForPendingFinalizers();
            //StringBuilder sbResult = new StringBuilder(jirehService.HttpDownload(CV_CTS_INFO_SIN.CONS_TP, CV_CTS_INFO_SIN.CONS_PATH.Split(new string[] { @"\"},StringSplitOptions.None)[3]));
            ////string sResult = jirehService.HttpDownload("Movie", "test2");
            //string sPrevPath = StaticUtils.DeCompressFolder(Convert.FromBase64String(sbResult.ToString()), "TempContents", @"D:\Contents");


            ////string[] arrPrevImg = Directory.GetFiles(sPrevPath);

            //Process.Start(sPrevPath);
            //System.GC.Collect();
            //System.GC.WaitForPendingFinalizers();
            //using (var fileStream = File.Create(@"D:\test2.png"))
            //{
            //    fsResult.Seek(0, SeekOrigin.Begin);
            //    fsResult.CopyTo(fileStream);
            //    fsResult.Close();
            //}

            //File.WriteAllBytes(@"D:\test.mkv", Encoding.ASCII.GetBytes((new IMRUtils.Encryption()).AESDecrypt256(jirehService.HttpDownload(@"Movie\Watch in 360 the inside of a nuclear reactor from the size of an atom with virtual reality.mkv"))));

            //using (System.IO.FileStream output = new FileStream(@"D:\"+ sFileName, FileMode.Create))
            //{
            //    int readBytes = 0;
            //    while ((readBytes = fsResult.Read(buffer, 0, buffer.Length)) > 0)
            //    {
            //        output.Write(buffer, 0, readBytes);
            //    }
            //}

            // 구매이력 추가

            InsertPrchHistory(MainViewModel.LoginUser.USER_NO, CV_CTS_INFO_SIN.CONS_ID);

            // 구매내역 갱신
            MainViewModel.Instance.InitPrchList();
            // 다운로드 완료 후 프로그램 설치 or 동영상 실행
        }

        void fileDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {

            // e.BytesReceived
            //   받은 데이터의 크기를 저장합니다.

            // e.TotalBytesToReceive
            //   받아야 할 모든 데이터의 크기를 저장합니다.

            //DownloadPersent = (int)((e.BytesReceived * 100) / e.TotalBytesToReceive);
            DownloadPersent = e.ProgressPercentage;
        }

        void fileDownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            ContentsPrice = "완료";
            string sDesFilePath = @"D:\Contents\" + CV_CTS_INFO_SIN.CONS_PATH.Split(new string[] { @"\" }, StringSplitOptions.None)[3];
            Process.Start(sDesFilePath);
            DownloadPersent = 0;
            wcDownload.Dispose();
        }

        /// <summary>
        /// 구매 이력 등록 함수
        /// </summary>
        /// <param name="sUserId"></param>
        /// <param name="sConsID"></param>
        /// <returns></returns>
        private bool InsertPrchHistory(string sUserId, string sConsID)
        {
            DataSet dsResult = new DataSet();

            //dsResult = callQuery.S_MA_USR_FAV_LIST(MainViewModel.LoginUser.USER_NO);
            dsResult = callQuery.I_MA_USR_PRCH_SIN(sUserId, sConsID);

            //실패시 false 리턴
            if ("Y" == dsResult.Tables[0].Rows[0][0].ToString())
            {
                return false;
            }

            return true;
        }

        #endregion

        #endregion

        #region 통신 관련

        /// <summary>
        /// 테스트 파라미터 만들기
        /// </summary>
        /// <returns></returns>
        //public DataSet koo()
        //{
        //    // XML(string) 형태의 결과 값 저장 변수
        //    string sResult = string.Empty;
        //    // DataSet 변환 후 결과 값 저장 변수
        //    DataSet dsResult;

        //    // 서비스 정보 및 파라미터 담을 ServiceInfo 객체 생성
        //    IMRUtils.UdtServiceInfo udtServiceInfo = Service.CreateServiceInfo(Constant.Service.ServiceName.SELECT.S02, null);

        //    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //    /////// 파라미터 있을 경우 예시                                                                                                                          /////
        //    ///////IMRUtils.UdtServiceInfo udtServiceInfo = Service.CreateServiceInfo(Constant.Service.ServiceName.SELECT.S02, new Dictionary<string, string>()   /////
        //    ///////{                                                                                                                                              /////
        //    ///////    { "키1","값1" }                                                                                                                             /////
        //    ///////    ,{ "키2","값2" }                                                                                                                            /////
        //    ///////});                                                                                                                                            /////
        //    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        //    // 파라미터 없음
        //    // udtServiceInfo.DicParams = 없어

        //    // 서비스 호출 정보 생성
        //    string sServiceParams = createParameter.CreateParams(udtServiceInfo);

        //    // 서비스 호출
        //    sResult = Service.CallService(sServiceParams);

        //    // DataSet으로 변환
        //    dsResult = IMRUtils.TypeParser.XmlToDataSet(sResult);

        //    // 결과 값 반환
        //    return dsResult;
        //}

        #endregion

        //#region 다운로드 관련 대리자
        //void CrossSafeSetValueMethod(Int32 value)
        //{
        //    if (prgDownload.InvokeRequired)
        //        prgDownload.Invoke(cssm, value);
        //    else
        //        prgDownload.Value = value;
        //}
        //void CrossSafeSetMaximumMethod(Int32 value)
        //{
        //    if (prgDownload.InvokeRequired)
        //        prgDownload.Invoke(cssm, value);
        //    else
        //        prgDownload.Maximum = value;
        //}
        //void CrossSafeSetTextMethod(String text)
        //{
        //    if (this.InvokeRequired)
        //        this.Invoke(csst, text);
        //    else
        //        this.Text = text;
        //}
        //#endregion
    }
}