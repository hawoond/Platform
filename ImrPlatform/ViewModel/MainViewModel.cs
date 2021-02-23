using ImrPlatform.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Data;
using System.Windows.Threading;
using System.Windows.Data;
using static IMRUtils.OverrideType;
using System.Windows.Input;
using IMRUtils.WPF;
using ImrPlatform.View.Main;
using System.Xml.Linq;
using System.Xml;
using ImrPlatform.View.Login;
using EntityConnecter.Models.DB.SELECT;
using System.Windows;
using ImrPlatform.Communication;
using ImrPlatform.Common;
using ImrPlatform.View.Contents;

namespace ImrPlatform.ViewModel
{
    public class MainViewModel : MomViewModel
    {
        public static Communication.ComnService Service;
        private IMRUtils.CreateParameter createParameter = new IMRUtils.CreateParameter();
        private ucPopup popup;

        private CallQuery callQuery;

        public static S_CO_USR_LGN_SIN LoginUser = null;

        #region 생성 및 초기화
        private MTObservableCollection<UdtContentItem> mContentList;
        public MTObservableCollection<UdtContentItem> ContentList
        {
            get
            {
                return mContentList ?? (mContentList = new MTObservableCollection<UdtContentItem>());
            }
            set
            {
                mContentList = value;
            }
        }

        /// <summary>
        /// 싱글턴 객체
        /// </summary>
        private static MainViewModel instance;
        public static MainViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MainViewModel();
                }
                return instance;
            }
        }

        /// <summary>
        /// 생성자
        /// </summary>
        public MainViewModel()
        {
            // 서비스 통신 객체 null 체크
            if (null == Service)
            {
                // 서비스 통신 객체 생성
                Service = new Communication.ComnService();
            }

            Init();
        }
        #endregion

        #region 초기화 관련

        /// <summary>
        /// 초기화 함수
        /// </summary>
        public void Init()
        {
            IsLogin = false;
            ContentList = new MTObservableCollection<UdtContentItem>();
            callQuery = new CallQuery();

            //ContentList.Add(test);
            IsSidebarOpen = null;

            SearchContent("1", "", "");

            FavList = new MTObservableCollection<UdtMyListItem>();
            PrchList = new MTObservableCollection<UdtMyListItem>();
            ReviewList = new MTObservableCollection<UdtMyListItem>();

            //패스워드 저장되어있으면 자동로그인함
            if (!string.IsNullOrEmpty(Properties.Settings.Default.PW))
            {
                AutoLogin();
            }
            else
            {
                LoginText = Properties.Resources.LOGIN_TEXT;
                UserName = Properties.Resources.LOGIN_TEXT;
            }

            InitMyList();
            InitPrchList();
            InitReviewList();
        }

        /// <summary>
        /// 마이리스트 조회
        /// </summary>
        public void InitMyList()
        {
            //로그인 예외처리
            //테스트 끝나면 풀것
            if (null == MainViewModel.LoginUser)
            {
                return;
            }

            DataSet dsResult = new DataSet();

            dsResult = callQuery.S_MA_USR_FAV_LIST(MainViewModel.LoginUser.USER_NO);
            //dsResult = callQuery.S_MA_USR_FAV_LIST("wildowl");

            if (0 == dsResult.Tables.Count)
            {
                return;
            }

            FavList.Clear();
            foreach (DataRow item in dsResult.Tables[0].Rows)
            {
                UdtMyListItem myItem = new UdtMyListItem();

                myItem.ContentID = item["CONS_ID"].ToString();
                myItem.ContentName = item["CONS_NM"].ToString();

                System.Drawing.Image thumbImg = IMRUtils.TypeParser.ByteArrayToImage(Convert.FromBase64String(item["CONS_THUMB_PATH"].ToString()));
                myItem.ContentThumbnail = StaticUtils.ImageToImageSource(thumbImg);

                myItem.ContentType = item["CONS_TP"].ToString();
                myItem.EnterDtm = item["FAV_DTM"].ToString();
                myItem.Deleterable = System.Windows.Visibility.Visible;

                FavList.Add(myItem);
            }
        }

        /// <summary>
        /// 구매내역 리스트 조회
        /// </summary>
        public void InitPrchList()
        {
            //로그인 예외처리
            //테스트 끝나면 풀것
            if (null == MainViewModel.LoginUser)
            {
                return;
            }

            DataSet dsResult = new DataSet();

            dsResult = callQuery.S_MA_USR_PRCH_LIST(MainViewModel.LoginUser.USER_NO);
            //dsResult = callQuery.S_MA_USR_PRCH_LIST("wildowl");

            if (0 == dsResult.Tables.Count)
            {
                return;
            }

            PrchList.Clear();
            foreach (DataRow item in dsResult.Tables[0].Rows)
            {
                UdtMyListItem myItem = new UdtMyListItem();

                myItem.ContentID = item["CONS_ID"].ToString();
                myItem.ContentName = item["CONS_NM"].ToString();

                System.Drawing.Image thumbImg = IMRUtils.TypeParser.ByteArrayToImage(Convert.FromBase64String(item["CONS_THUMB_PATH"].ToString()));
                myItem.ContentThumbnail = StaticUtils.ImageToImageSource(thumbImg);

                myItem.ContentType = item["CONS_TP"].ToString();
                myItem.EnterDtm = item["PRCH_DTM"].ToString();
                myItem.Deleterable = System.Windows.Visibility.Collapsed;

                PrchList.Add(myItem);
            }
        }

        /// <summary>
        /// 리뷰 리스트 조회
        /// </summary>
        public void InitReviewList()
        {
            //로그인 예외처리
            //테스트 끝나면 풀것
            if (null == MainViewModel.LoginUser)
            {
                return;
            }

            DataSet dsResult = new DataSet();

            dsResult = callQuery.S_MA_USR_REV_LIST(MainViewModel.LoginUser.USER_NO);
            //dsResult = callQuery.S_MA_USR_REV_LIST("wildowl");

            if (0 == dsResult.Tables.Count)
            {
                return;
            }

            ReviewList.Clear();
            foreach (DataRow item in dsResult.Tables[0].Rows)
            {
                UdtMyListItem myItem = new UdtMyListItem();

                myItem.ContentID = item["CONS_ID"].ToString();
                myItem.ContentName = item["CONS_NM"].ToString();

                System.Drawing.Image thumbImg = IMRUtils.TypeParser.ByteArrayToImage(Convert.FromBase64String(item["CONS_THUMB_PATH"].ToString()));
                myItem.ContentThumbnail = StaticUtils.ImageToImageSource(thumbImg);

                myItem.ContentType = item["CONS_TP"].ToString();
                myItem.EnterDtm = item["REV_DTM"].ToString();
                myItem.Deleterable = System.Windows.Visibility.Collapsed;

                ReviewList.Add(myItem);
            }
        }

        /// <summary>
        /// 자동로그인!!!!
        /// </summary>
        private void AutoLogin()
        {
            CallQuery callQuery = new CallQuery();
            DataSet dsResult = callQuery.S_CO_USR_LGN_SIN(Properties.Settings.Default.ID, Properties.Settings.Default.PW);

            DataRow LoginData = dsResult.Tables[0].Rows[0];
            if (LoginData["SUCCESS_YN"].Equals("Y"))
            {
                MainViewModel.LoginUser = (S_CO_USR_LGN_SIN)StaticUtils.DataRowToClass(new S_CO_USR_LGN_SIN(), LoginData);
                LoginText = Properties.Resources.LOGOUT_TEXT;
                IsLogin = true;
                UserName = LoginUser.USER_NM;
                UserNumber = LoginUser.USER_NO;
            }
            else
            {
                MessageBox.Show("로그인 정보 변경됐당 다시 로그인해랑");
            }
        }
        #endregion

        #region 커맨드 변수
        private ICommand ISearchCommand;
        private ICommand ILoginCommand;
        private ICommand ISelectedItemCommand;
        //private ICommand IExitCommand;

        /// <summary>
        /// 카테고리 선택 토글
        /// </summary>
        private ICommand ICatToggleCommand;

        #endregion

        #region 커맨드

        public ICommand SearchCommand
        {
            get
            {
                if (null == ISearchCommand)
                {
                    ISearchCommand = new DelegateCommand(ContentSearch);
                }
                return ISearchCommand;
            }
        }
        public ICommand LoginCommand
        {
            get
            {
                if (null == ILoginCommand)
                {
                    ILoginCommand = new DelegateCommand(PopupLoginPage);
                }
                return ILoginCommand;
            }
        }

        public ICommand SelectedItemCommand
        {
            get
            {
                if (null == ISelectedItemCommand)
                {
                    ISelectedItemCommand = new DelegateCommand(SelectedItem);
                }
                return ISelectedItemCommand;
            }
        }

        //public ICommand ExitCommand
        //{
        //    get
        //    {
        //        if (null == IExitCommand)
        //        {
        //            IExitCommand = new DelegateCommand(Exit);
        //        }
        //        return IExitCommand;
        //    }
        //}

        /// <summary>
        /// 카테고리 선택 토글 버튼 커맨드
        /// </summary>
        public ICommand CatToggleCommand
        {
            get
            {
                if (ICatToggleCommand == null)
                {
                    ICatToggleCommand = new DelegateCommand(SelectCategory);
                }
                return ICatToggleCommand;
            }
        }

        private void ContentSearch(object obj)
        {
            SearchContent("1", SearchText, "");
        }



        private void PopupLoginPage(object obj)
        {
            if (LoginUser == null)
            {
                bool? result = Common.StaticUtils.ShowLoginWindow();
                if (result.Equals(true))
                {
                    LoginText = Properties.Resources.LOGOUT_TEXT;
                    IsLogin = true;
                    UserName = LoginUser.USER_NM;
                    UserNumber = LoginUser.USER_NO;
                }
                else
                {
                    LoginText = Properties.Resources.LOGIN_TEXT;
                    IsLogin = false;
                    UserName = string.Empty;
                    UserNumber = string.Empty;
                }
            }
            else
            {
                string messageBoxText = "로그아웃 했다";
                string caption = "랄라";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show(messageBoxText, caption, button, icon);
                LoginUser = null;
                LoginText = "로그인";
                IsLogin = false;
                UserName = "";
                UserNumber = string.Empty;
            }
        }

        /// <summary>
        /// 카테고리 토글 버튼 선택 함수
        /// </summary>
        /// <param name="obj"></param>
        public void SelectCategory(object obj)
        {
            SearchContent("1", "", obj.ToString());
        }

        //public void Exit(object obj)
        //{
        //    Environment.Exit(0);
        //}

        #endregion

        #region 내부 전역 변수
        private MTObservableCollection<UdtMyListItem> mFavList;
        private MTObservableCollection<UdtMyListItem> mPrchList;
        private MTObservableCollection<UdtMyListItem> mReviewList;
        private string mSearchText;
        private string sLoginText;
        private bool isLogin;
        private string sUserName;
        private string sUserNumber;
        #endregion

        #region 바인딩 적용 변수
        public MTObservableCollection<UdtMyListItem> FavList
        {
            get
            {
                return mFavList;
            }
            set
            {
                mFavList = value;
                this.OnPropertyChanged("FavList");
            }
        }

        public MTObservableCollection<UdtMyListItem> PrchList
        {
            get
            {
                return mPrchList;
            }
            set
            {
                mPrchList = value;
                this.OnPropertyChanged("PrchList");
            }
        }

        public MTObservableCollection<UdtMyListItem> ReviewList
        {
            get
            {
                return mReviewList;
            }
            set
            {
                mReviewList = value;
                this.OnPropertyChanged("ReviewList");
            }
        }

        /// <summary>
        /// 검색 문자열 바인딩 변수
        /// </summary>
        public string SearchText
        {
            get
            {
                return mSearchText;
            }
            set
            {
                mSearchText = value.ToString();
                this.OnPropertyChanged("SearchText");
            }
        }

        public string LoginText
        {
            get
            {
                return sLoginText;
            }
            set
            {
                sLoginText = value.ToString();
                this.OnPropertyChanged("LoginText");
            }
        }

        public bool IsLogin
        {
            get
            {
                return isLogin;
            }
            set
            {
                isLogin = value;
                this.OnPropertyChanged("IsLogin");
            }
        }

        public string UserName
        {
            get
            {
                return sUserName;
            }
            set
            {
                if (value.ToString().Equals(""))
                {
                    UserName = Properties.Resources.LOGIN_TEXT;
                }
                else
                {
                    sUserName = value.ToString();
                }
                this.OnPropertyChanged("UserName");
            }
        }

        public string UserNumber
        {
            get
            {
                return sUserNumber;
            }
            set
            {
                sUserNumber = value.ToString();
                this.OnPropertyChanged("UserNumber");
            }
        }
        #endregion

        #region 사이드메뉴 관련
        /// <summary>
        /// 사이드메뉴 커맨드
        /// </summary>
        private ICommand ISidebarOpenCommand;
        public ICommand SidebarOpenCommand
        {
            get
            {
                if (ISidebarOpenCommand == null)
                {
                    ISidebarOpenCommand = new DelegateCommand(SidebarOpen);

                }
                return ISidebarOpenCommand;
            }
        }

        private ICommand IOpenListPopupCommand;
        public ICommand OpenListPopupCommand
        {
            get
            {
                if (IOpenListPopupCommand == null)
                {
                    IOpenListPopupCommand = new DelegateCommand(OpenListPopup);

                }
                return IOpenListPopupCommand;
            }
        }

        private bool? isSidebarOpen;
        public bool? IsSidebarOpen
        {
            get
            {
                return isSidebarOpen;
            }
            set
            {
                isSidebarOpen = value;
                this.OnPropertyChanged("IsSidebarOpen");
            }
        }

        private void SidebarOpen(object obj)
        {
            if (IsSidebarOpen != null)
            {
                if ((bool)IsSidebarOpen)
                {
                    IsSidebarOpen = false;
                }
                else
                {
                    IsSidebarOpen = true;
                }
            }
            else
            {
                IsSidebarOpen = true;
            }
        }

        private void OpenListPopup(object obj)
        {
            int nFlag = int.Parse(obj.ToString());
            switch (nFlag)
            {
                case 0:
                {
                    break;
                }
                case 1:
                {
                    break;
                }
                case 2:
                {
                    break;
                }
                case 3:
                {
                    break;
                }
                default:
                {
                    break;
                }
            }
        }
        #endregion

        #region 팝업 관련
        private ICommand IPopupOkCommand;
        public ICommand PopupOkCommand
        {
            get
            {
                if (null == IPopupOkCommand)
                {
                    IPopupOkCommand = new DelegateCommand(PopupOk);
                }
                return IPopupOkCommand;
            }
        }

        public void PopupOk(object param)
        {
            if (popup != null)
            {
                popup.Close();
            }
        }

        private ICommand IPopupCancelCommand;
        public ICommand PopupCancelCommand
        {
            get
            {
                if (null == IPopupCancelCommand)
                {
                    IPopupCancelCommand = new DelegateCommand(PopupClose);
                }
                return IPopupCancelCommand;
            }
        }

        public void PopupClose(object param)
        {
            if (popup != null)
            {
                popup.Close();
            }
        }

        #endregion

        private void SelectedItem(object obj)
        {
            string contId = ((UdtContentItem)obj).ContentID;
            (new ucContentsMain(contId)).Show();
        }

        /// <summary>
        /// 테스트 파라미터 만들기
        /// </summary>
        /// <returns></returns>
        public DataSet koo()
        {
            // XML(string) 형태의 결과 값 저장 변수
            string sResult = string.Empty;
            // DataSet 변환 후 결과 값 저장 변수
            DataSet dsResult;

            // 서비스 정보 및 파라미터 담을 ServiceInfo 객체 생성
            //IMRUtils.UdtServiceInfo udtServiceInfo = Service.CreateServiceInfo(Constant.Service.ServiceName.SELECT.S02, null);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////// 파라미터 있을 경우 예시                                                                                                                          /////
            ///////IMRUtils.UdtServiceInfo udtServiceInfo = Service.CreateServiceInfo(Constant.Service.ServiceName.SELECT.S02, new Dictionary<string, string>()   /////
            ///////{                                                                                                                                              /////
            ///////    { "키1","값1" }                                                                                                                             /////
            ///////    ,{ "키2","값2" }                                                                                                                            /////
            ///////});                                                                                                                                            /////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // 파라미터 있을 경우 예시                                                                                                                      
            IMRUtils.UdtServiceInfo udtServiceInfo = Service.CreateServiceInfo(Constant.Service.ServiceName.SELECT.CV_CTS_INFO_SIN, new Dictionary<string, string>()
            {
                { "CONS_ID","OWL" }
            });

            // 파라미터 없음
            // udtServiceInfo.DicParams = 없어

            IMRUtils.Encryption encryption = new IMRUtils.Encryption();

            // 서비스 호출 정보 생성
            string sServiceParams = encryption.AESEncrypt256(createParameter.CreateParams(udtServiceInfo));

            // 서비스 호출
            sResult = Service.CallService(sServiceParams);

            // DataSet으로 변환
            dsResult = IMRUtils.TypeParser.XmlToDataSet(sResult);
            XmlReader xmlReader = XDocument.Parse(sResult).CreateReader();

            // 결과 값 반환
            return dsResult;
        }

        private void SetListData()
        {
            int 총_갯수 = 50;
            string 임시_문자열_저장_공간 = string.Empty;
            for (int 시작_숫자 = 0; 시작_숫자 < 총_갯수; 시작_숫자++)
            {
                임시_문자열_저장_공간 += 시작_숫자.ToString() + " ";
            }
        }


        private void SearchContent(string sPage, string sName, string sCategory)
        {
            ContentList.Clear();

            DataSet dsContsResult = callQuery.S_MA_CTS_SRCH_LIST(sPage, sName, sCategory);

            UdtContentItem udtResult;

            for (int i = 0; i < dsContsResult.Tables[0].Rows.Count; i++)
            {
                udtResult = new UdtContentItem();

                udtResult.ContentID = dsContsResult.Tables[0].Rows[i]["CONS_ID"].ToString();
                udtResult.ContentTitle = dsContsResult.Tables[0].Rows[i]["CONS_NM"].ToString();
                udtResult.ContentDesc = dsContsResult.Tables[0].Rows[i]["CONS_DOC"].ToString();
                udtResult.CopyWriterName = dsContsResult.Tables[0].Rows[i]["COPY_NM"].ToString();
                udtResult.DownloadRate = dsContsResult.Tables[0].Rows[i]["DOWN_RATE"].ToString();
                udtResult.Price = dsContsResult.Tables[0].Rows[i]["CONS_PRICE"].ToString();
                udtResult.StarRate = dsContsResult.Tables[0].Rows[i]["STAR_RATE"].ToString();
                System.Drawing.Image thumbnailImg = IMRUtils.TypeParser.ByteArrayToImage(Convert.FromBase64String(dsContsResult.Tables[0].Rows[i]["CONS_THUMB_PATH"].ToString()));
                udtResult.Thumbnail = StaticUtils.ImageToImageSource(thumbnailImg);

                ContentList.Add(udtResult);
            }
        }

        //private UdtMyListItem myListRowSelect;
        //public UdtMyListItem MyListRowSelect
        //{
        //    get { return myListRowSelect; }
        //    set
        //    {
        //        myListRowSelect = value;
        //        OnPropertyChanged("MyListRowSelect");

        //        //여기서 삭제

        //    }
        //}
    }
}