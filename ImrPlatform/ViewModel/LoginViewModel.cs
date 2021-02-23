using EntityConnecter.Models.DB.SELECT;
using ImrPlatform.Common;
using ImrPlatform.Communication;
using ImrPlatform.Properties;
using IMRUtils.WPF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ImrPlatform.ViewModel
{
    class LoginViewModel : IMRUtils.WPF.MomViewModel
    {
        /// <summary>
        /// 싱글턴 객체
        /// </summary>
        private static LoginViewModel instance;
        public static LoginViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LoginViewModel();
                }
                return instance;
            }
        }

        /// <summary>
        /// 생성자
        /// </summary>
        public LoginViewModel()
        {
            Init();
        }

        /// <summary>
        /// 초기화
        /// </summary>
        private void Init()
        {
            LoginID = Properties.Settings.Default.ID;
            if (!string.IsNullOrEmpty(LoginID))
            {
                AutoIDSave = true;
            }
        }

        #region private변수
        private ICommand ILoginCommand;
        private ICommand IExitLoginWindow;
        private string loginID;
        private string password;
        private bool bAutoLogin;
        private bool bAutoIDSave;
        #endregion

        #region 변수 선언
        public ICommand LoginCommand
        {
            get
            {
                if (ILoginCommand == null)
                {
                    ILoginCommand = new DelegateCommand(Login);
                }
                return ILoginCommand;
            }
        }

        public ICommand ExitLoginWindow
        {
            get
            {
                if (IExitLoginWindow == null)
                {
                    IExitLoginWindow = new DelegateCommand(Exit);
                }
                return IExitLoginWindow;
            }
        }

        public string LoginID
        {
            get
            {
                return loginID;
            }
            set
            {
                loginID = value;
                this.OnPropertyChanged("LoginID");
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                this.OnPropertyChanged("Password");
            }
        }

        public bool AutoLogin
        {
            get
            {
                return bAutoLogin;
            }
            set
            {
                bAutoLogin = value;
                this.OnPropertyChanged("AutoLogin");
            }
        }

        public bool AutoIDSave
        {
            get
            {
                return bAutoIDSave;
            }
            set
            {
                bAutoIDSave = value;
                this.OnPropertyChanged("AutoIDSave");
            }
        }

        #endregion

        private void Login(object param)
        {
            CallQuery callQuery = new CallQuery();
            DataSet dsResult = callQuery.S_CO_USR_LGN_SIN(LoginID, Password);

            DataRow LoginData = dsResult.Tables[0].Rows[0];
            if (LoginData["SUCCESS_YN"].Equals("Y"))
            {
                MainViewModel.LoginUser = (S_CO_USR_LGN_SIN)StaticUtils.DataRowToClass(new S_CO_USR_LGN_SIN(), LoginData);

                if (bAutoLogin)
                {
                    Properties.Settings.Default.ID = LoginID;
                    Properties.Settings.Default.PW = Password;
                }
                else
                {
                    Properties.Settings.Default.ID = "";
                    Properties.Settings.Default.PW = "";
                }

                if (bAutoIDSave)
                {
                    Properties.Settings.Default.ID = LoginID;
                }
                else
                {
                    Properties.Settings.Default.ID = "";
                }

                Properties.Settings.Default.Save();
                StaticUtils.loginWindow.DialogResult = true;
                StaticUtils.CloseLoginWindow();
            }
            else
            {
                MessageBox.Show("로그인 안됐당");
            }
        }

        private void Exit(object obj)
        {
            StaticUtils.loginWindow.DialogResult = false;
            StaticUtils.CloseLoginWindow();
        }

    }
}
