using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Controls;
using System.Threading;

namespace Codenesium.DatabaseForgeLib.UserControls
{
    public partial class AbstractUserControl : MetroUserControl
    {
            protected List<String> LogMessages { get; private set; } = new List<String>();
            private SemaphoreSlim _logSemaphore = new SemaphoreSlim(1, 1);
            public AbstractUserControl()
            {
            this.InitializeComponent();
            }

            public void showSpinner()
            {
                Action showSpinner = () =>
                {
                    this.progressSpinnerDefault.Visible = true;
                };

                if (this.progressSpinnerDefault.InvokeRequired)
                {
                    this.progressSpinnerDefault.Invoke(new MethodInvoker(delegate
                    {
                        showSpinner();
                    }));
                }
                else
                {
                    showSpinner();
                }
            }

            public void hideSpinner()
            {
                Action hideSpinner = () =>
                {
                    this.progressSpinnerDefault.Visible = false;
                };

                if (this.progressSpinnerDefault.InvokeRequired)
                {
                    this.progressSpinnerDefault.Invoke(new MethodInvoker(delegate
                    {
                        hideSpinner();
                    }));
                }
                else
                {
                    hideSpinner();
                }
            }

            public void displayUserMessageHide()
            {

                Action hideMessage = () =>
                {
                    this.labelUserMessage.Text = string.Empty;
                    this.labelUserMessage.Visible = false;
                };

                if (this.labelUserMessage.InvokeRequired)
                {
                    this.labelUserMessage.Invoke(new MethodInvoker(delegate
                    {
                        hideMessage();
                    }));
                }
                else
                {
                    hideMessage();
                }
            }


            public void displayUserMessageSuccess(string text)
            {
                this.AddLogMessage(text);
                Action displaySuccess = () =>
                {
                    this.labelUserMessage.ForeColor = Color.SeaGreen;
                    this.labelUserMessage.Text = text;
                    this.labelUserMessage.Visible = true;
                };

                if (this.labelUserMessage.InvokeRequired)
                {
                    this.labelUserMessage.Invoke(new MethodInvoker(delegate
                    {
                        displaySuccess();
                    }));
                }
                else
                {
                    displaySuccess();
                }
            }

            public void displayUserMessageFailure(string text)
            {
                this.AddLogMessage(text);
                Action displayFailure = () =>
                {
                    this.labelUserMessage.ForeColor = Color.Red;
                    this.labelUserMessage.Text = text;
                    this.labelUserMessage.Visible = true;
                };

                if (this.labelUserMessage.InvokeRequired)
                {
                    this.labelUserMessage.Invoke(new MethodInvoker(delegate
                    {
                        displayFailure();
                    }));
                }
                else
                {
                    displayFailure();
                }
            }

            private void AddLogMessage(string message)
            {
                this._logSemaphore.Wait();
            string last = this.LogMessages.LastOrDefault();
                if (last == null || last != message)
                {
                    this.LogMessages.Add(message);
                }
                this._logSemaphore.Release();
            }

            private void AbstractUserControl_VisibleChanged(object sender, EventArgs e)
            {
                if (this.Visible)
                {
                    this.displayUserMessageHide();
                }
            }
        }
}
