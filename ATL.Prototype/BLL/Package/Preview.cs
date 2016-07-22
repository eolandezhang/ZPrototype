using BLL.Settings;
using Model.Package;
using Model.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace BLL.Package
{
    public class Preview
    {
        readonly DAL.Settings.Users _Users = new DAL.Settings.Users();
        readonly DAL.Package.PACKAGE_BASE_INFO _PACKAGE_BASE_INFO = new DAL.Package.PACKAGE_BASE_INFO();
        readonly DAL.Package.PACKAGE_WF_STEP _PACKAGE_WF_STEP = new DAL.Package.PACKAGE_WF_STEP();
        readonly DAL.Package.PACKAGE_WF_STEP_AUDITOR _PACKAGE_WF_STEP_AUDITOR = new DAL.Package.PACKAGE_WF_STEP_AUDITOR();
        readonly DAL.Settings.WF_SET_STEP _WF_SET_STEP = new DAL.Settings.WF_SET_STEP();
        readonly DAL.Settings.PMES_USER_GROUP_INFO _PMES_USER_GROUP_INFO = new DAL.Settings.PMES_USER_GROUP_INFO();
        readonly DAL.Package.Preview _Preview = new DAL.Package.Preview();
        public bool HasBeginWf(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID)
        {
            return _PACKAGE_WF_STEP.GetDataByPkgId(PACKAGE_NO, VERSION_NO, FACTORY_ID).Any();
        }

        //开启审批流程        
        public int Init_PACKAGE_WF_STEP(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID)
        {
            if (!BLL.Settings.Permission.CheckStartWFRight(PACKAGE_NO, FACTORY_ID, VERSION_NO)) return -1;//如果是制作人，则有权限
            int result = 0;
            if (_PACKAGE_WF_STEP.GetDataByPkgId(PACKAGE_NO, VERSION_NO, FACTORY_ID).Any()) return 1;//如果有审批步骤，则已经开启
            //新增审批流程
            //1.找到PACKAGE_BASE_INFO选中的审批流程APPROVE_FLOW_ID
            var PACKAGE_BASE_INFO = new DAL.Package.PACKAGE_BASE_INFO().GetDataById(PACKAGE_NO, FACTORY_ID,
                VERSION_NO, "");
            if (!PACKAGE_BASE_INFO.Any()) return 0;
            string APPROVE_FLOW_ID = PACKAGE_BASE_INFO.First().APPROVE_FLOW_ID;
            //2.找到相应的WF_SET记录
            var WF_SET = new DAL.Settings.WF_SET().GetDataById(APPROVE_FLOW_ID, FACTORY_ID, "");
            if (!WF_SET.Any()) return 0;
            string WF_SET_NUM = WF_SET.First().WF_SET_NUM;
            //3.找到相应的WF_SET_STEP记录，找到STEP_FLAG='FST'的记录
            var WF_SET_STEP = new DAL.Settings.WF_SET_STEP().GetDataBySetId(WF_SET_NUM, FACTORY_ID, "").Where(x => x.STEP_FLAG == "FST");
            if (!WF_SET.Any()) return 0;
            var WF_SET_STEP_ID = WF_SET_STEP.First().WF_SET_STEP_ID;
            //4.在PACKAGE_WF_STEP中，新增此步骤
            result = new BLL.Package.PACKAGE_WF_STEP().PostAdd(new PACKAGE_WF_STEP_Entity
            {
                FACTORY_ID = FACTORY_ID,
                PACKAGE_NO = PACKAGE_NO,
                VERSION_NO = VERSION_NO,
                WF_SET_STEP_ID = WF_SET_STEP_ID
            });
            //5.在PACKAGE_WF_STEP_AUDITOR中，新增当前用户
            if (result > 0)
            {
                new BLL.Package.PACKAGE_WF_STEP_AUDITOR().PostAdd(new PACKAGE_WF_STEP_AUDITOR_Entity
                {
                    PACKAGE_WF_STEP_ID = result,
                    PMES_USER_ID = PACKAGE_BASE_INFO.First().PREPARED_BY,
                    IS_AGREED = "0",
                    IS_CANCELED = "0"
                });
                //6.更新package的审批状态 status=2
                //new DAL.Package.PACKAGE_BASE_INFO().UpdateStatus(PACKAGE_NO, VERSION_NO, FACTORY_ID, "2");
            }

            return result;
        }

        //获取当前审批步骤
        public PACKAGE_WF_STEP_Entity GetCurrentPkgWfStep(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID)
        {
            var lst = _PACKAGE_WF_STEP.GetLatest(PACKAGE_NO, VERSION_NO, FACTORY_ID);
            if (!lst.Any()) return null;
            return lst.First();
        }
        //获取下一步审批步骤
        public WF_SET_STEP_Entity GetWfSetNextStep(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID)
        {
            var currentStep = GetCurrentPkgWfStep(PACKAGE_NO, VERSION_NO, FACTORY_ID);
            if (currentStep == null) return null;
            var package = _PACKAGE_BASE_INFO.GetDataById(PACKAGE_NO, FACTORY_ID, VERSION_NO, "");
            if (!package.Any()) return null;
            var lst = _WF_SET_STEP.GetDataById(currentStep.WF_SET_STEP_ID, package.First().APPROVE_FLOW_ID, FACTORY_ID, "");
            if (!lst.Any()) return null;
            var nextStepId = lst.First().AGREE_STEP_ID;
            var nextWfStep = _WF_SET_STEP.GetDataById(nextStepId, package.First().APPROVE_FLOW_ID, FACTORY_ID, "");
            return nextWfStep.Any() ? nextWfStep.First() : null;
        }
        //获取审批者列表
        public List<PMES_USER_GROUP_INFO_Entity> GetAuditors(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID)
        {
            var nextStep = GetWfSetNextStep(PACKAGE_NO, VERSION_NO, FACTORY_ID);
            if (nextStep == null) return null;
            var PMES_USER_GROUP_ID = nextStep.PMES_USER_GROUP_ID;
            var users = _PMES_USER_GROUP_INFO.GetDataByGroupId(PMES_USER_GROUP_ID, FACTORY_ID, "");
            return users;
        }
        //同意
        public int Agree(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string PMES_USER_ID, string AUDITOR_COMMENT)
        {
            //判断当前步骤签审人是否是当前用户
            var package = _PACKAGE_BASE_INFO.GetDataById(PACKAGE_NO, FACTORY_ID, VERSION_NO, "");
            if (!package.Any()) return 0;
            var wfSetStepId = package.First().APPROVE_FLOW_ID;
            var currentUser = new Users().GetCurrentUser();
            //if (!package.First().PREPARED_BY.Equals(currentUser.DESCRIPTION)) return -1;

            //获取当前审批步骤 PACKAGE_WF_STEP
            var currentPkgWfStep = GetCurrentPkgWfStep(PACKAGE_NO, VERSION_NO, FACTORY_ID);
            if (currentPkgWfStep == null) return 0;
            //获取当前步骤的所有审批者,如果当前审核者不是其中之一，则返回-1
            var currentPkgWfStepAuditorNotCanceled = _PACKAGE_WF_STEP_AUDITOR.GetDataByPkgStepId(currentPkgWfStep.PACKAGE_WF_STEP_ID).Where(x => x.IS_CANCELED != "1" && x.PMES_USER_ID.Equals(currentUser.DESCRIPTION));
            if (!currentPkgWfStepAuditorNotCanceled.Any()) return -1;
            var pkgWfStepAuditor = currentPkgWfStepAuditorNotCanceled.First();
            //获取同意的审批步骤 WF_SET_STEP
            var wfSetCurrentStep = _WF_SET_STEP.GetDataById(currentPkgWfStep.WF_SET_STEP_ID, wfSetStepId, FACTORY_ID, "");
            if (!wfSetCurrentStep.Any()) return 0;
            var wfSetNextStepId = wfSetCurrentStep.First().AGREE_STEP_ID;
            if (!string.IsNullOrEmpty(wfSetNextStepId))//如果有下一步
            {
                var wfSetNextStepList = _WF_SET_STEP.GetDataById(wfSetNextStepId, wfSetStepId, FACTORY_ID, "");
                if (!wfSetNextStepList.Any()) return 0;
                var wfSetNextStep = wfSetNextStepList.First();
                if (!wfSetNextStep.STEP_FLAG.Equals("LST"))//如果不是最后一步
                {
                    if (string.IsNullOrEmpty(PMES_USER_ID)) return -2;

                    //更新当前审核人审批状态及备注 PACKAGE_WF_STEP_AUDITOR 的 IS_AGREED=1
                    new BLL.Package.PACKAGE_WF_STEP_AUDITOR().PostEdit(new PACKAGE_WF_STEP_AUDITOR_Entity
                    {
                        AUDITOR_ID = pkgWfStepAuditor.AUDITOR_ID,
                        PACKAGE_WF_STEP_ID = pkgWfStepAuditor.PACKAGE_WF_STEP_ID,
                        PMES_USER_ID = pkgWfStepAuditor.PMES_USER_ID,
                        IS_AGREED = "1",
                        AUDITOR_COMMENT = AUDITOR_COMMENT,
                        AUDIT_AT = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"),
                        IS_CANCELED = pkgWfStepAuditor.IS_CANCELED
                    });

                    //新增package审批步骤 PACKAGE_WF_STEP
                    var pkgWfNextStepId = new BLL.Package.PACKAGE_WF_STEP().PostAdd(new PACKAGE_WF_STEP_Entity
                    {
                        WF_SET_STEP_ID = wfSetNextStep.WF_SET_STEP_ID,
                        PACKAGE_NO = PACKAGE_NO,
                        VERSION_NO = VERSION_NO,
                        FACTORY_ID = FACTORY_ID
                    });

                    //新增package审批者 PACKAGE_WF_STEP_AUDITOR
                    new BLL.Package.PACKAGE_WF_STEP_AUDITOR().PostAdd(new PACKAGE_WF_STEP_AUDITOR_Entity
                    {
                        PACKAGE_WF_STEP_ID = pkgWfNextStepId,
                        PMES_USER_ID = PMES_USER_ID,
                        IS_AGREED = "0",
                        AUDITOR_COMMENT = "",
                        AUDIT_AT = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"),
                        IS_CANCELED = "0"
                    });
                    var user = _Users.GetDataByUserNum(PMES_USER_ID, "");
                    _PACKAGE_BASE_INFO.UpdateStatus(PACKAGE_NO, VERSION_NO, FACTORY_ID, "2");
                    SendAgreeEmail(PACKAGE_NO, VERSION_NO, FACTORY_ID, new[] { user.First().MAIL }, null);
                }
                else if (wfSetNextStep.STEP_FLAG.Equals("LST"))//是最后一步，仅仅增加步骤，不增加审批者
                {
                    //更新当前审核人审批状态及备注 PACKAGE_WF_STEP_AUDITOR 的 IS_AGREED=1
                    new BLL.Package.PACKAGE_WF_STEP_AUDITOR().PostEdit(new PACKAGE_WF_STEP_AUDITOR_Entity
                    {
                        AUDITOR_ID = pkgWfStepAuditor.AUDITOR_ID,
                        PACKAGE_WF_STEP_ID = pkgWfStepAuditor.PACKAGE_WF_STEP_ID,
                        PMES_USER_ID = pkgWfStepAuditor.PMES_USER_ID,
                        IS_AGREED = "1",
                        AUDITOR_COMMENT = AUDITOR_COMMENT,
                        AUDIT_AT = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"),
                        IS_CANCELED = pkgWfStepAuditor.IS_CANCELED
                    });

                    var pkgWfNextStepId = new BLL.Package.PACKAGE_WF_STEP().PostAdd(new PACKAGE_WF_STEP_Entity
                    {
                        WF_SET_STEP_ID = wfSetNextStep.WF_SET_STEP_ID,
                        PACKAGE_NO = PACKAGE_NO,
                        VERSION_NO = VERSION_NO,
                        FACTORY_ID = FACTORY_ID
                    });
                    //更新 status=5
                    _PACKAGE_BASE_INFO.UpdateStatus(PACKAGE_NO, VERSION_NO, FACTORY_ID, "5");
                    //将相同PACKAGE_NO的文件，其它版本的，STATUS=5 VALID_FLAG=1 的文件 ，更新VALID_FLAG=0
                    _PACKAGE_BASE_INFO.UpdateValidFlag(PACKAGE_NO, VERSION_NO, FACTORY_ID);
                }
            }

            return 1;
        }
        //不同意
        public int Disagree(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string AUDITOR_COMMENT)
        {
            //判断当前步骤签审人是否是当前用户
            var package = _PACKAGE_BASE_INFO.GetDataById(PACKAGE_NO, FACTORY_ID, VERSION_NO, "");
            if (!package.Any()) return 0;
            var wfSetStepId = package.First().APPROVE_FLOW_ID;
            var pkgEditor = package.First().PREPARED_BY;
            var currentUser = new Users().GetCurrentUser();
            //if (!package.First().PREPARED_BY.Equals(currentUser.DESCRIPTION)) return -1;

            //获取当前审批步骤 PACKAGE_WF_STEP
            var currentPkgWfStep = GetCurrentPkgWfStep(PACKAGE_NO, VERSION_NO, FACTORY_ID);
            if (currentPkgWfStep == null) return 0;
            if (currentPkgWfStep.STEP_FLAG == "FST") return -1;
            //获取当前步骤的所有审批者
            var currentPkgWfStepAuditorNotCanceled = _PACKAGE_WF_STEP_AUDITOR.GetDataByPkgStepId(currentPkgWfStep.PACKAGE_WF_STEP_ID).Where(x => x.IS_CANCELED != "1" && x.PMES_USER_ID.Equals(currentUser.DESCRIPTION));
            if (!currentPkgWfStepAuditorNotCanceled.Any()) return 0;
            var pkgWfStepAuditor = currentPkgWfStepAuditorNotCanceled.First();
            //获取不同意的审批步骤 WF_SET_STEP
            var wfSetCurrentStep = _WF_SET_STEP.GetDataById(currentPkgWfStep.WF_SET_STEP_ID, wfSetStepId, FACTORY_ID, "");
            if (!wfSetCurrentStep.Any()) return 0;
            var wfSetNextStepId = wfSetCurrentStep.First().DISAGREE_STEP_ID;
            if (!string.IsNullOrEmpty(wfSetNextStepId))//如果有下一步
            {
                var wfSetNextStepList = _WF_SET_STEP.GetDataById(wfSetNextStepId, wfSetStepId, FACTORY_ID, "");
                if (!wfSetNextStepList.Any()) return 0;
                var wfSetNextStep = wfSetNextStepList.First();
                if (wfSetNextStep.STEP_FLAG.Equals("FST"))//如果是第一步
                {
                    //更新当前审核人审批状态及备注 PACKAGE_WF_STEP_AUDITOR 的 IS_AGREED=0
                    new BLL.Package.PACKAGE_WF_STEP_AUDITOR().PostEdit(new PACKAGE_WF_STEP_AUDITOR_Entity
                    {
                        AUDITOR_ID = pkgWfStepAuditor.AUDITOR_ID,
                        PACKAGE_WF_STEP_ID = pkgWfStepAuditor.PACKAGE_WF_STEP_ID,
                        PMES_USER_ID = pkgWfStepAuditor.PMES_USER_ID,
                        IS_AGREED = "2",
                        AUDITOR_COMMENT = AUDITOR_COMMENT,
                        AUDIT_AT = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"),
                        IS_CANCELED = pkgWfStepAuditor.IS_CANCELED
                    });
                    //新增package审批步骤 PACKAGE_WF_STEP
                    var pkgWfNextStepId = new BLL.Package.PACKAGE_WF_STEP().PostAdd(new PACKAGE_WF_STEP_Entity
                    {
                        WF_SET_STEP_ID = wfSetNextStep.WF_SET_STEP_ID,
                        PACKAGE_NO = PACKAGE_NO,
                        VERSION_NO = VERSION_NO,
                        FACTORY_ID = FACTORY_ID
                    });
                    //新增package审批者 PACKAGE_WF_STEP_AUDITOR
                    new BLL.Package.PACKAGE_WF_STEP_AUDITOR().PostAdd(new PACKAGE_WF_STEP_AUDITOR_Entity
                    {
                        PACKAGE_WF_STEP_ID = pkgWfNextStepId,
                        PMES_USER_ID = pkgEditor,
                        IS_AGREED = "0",
                        AUDITOR_COMMENT = "",
                        AUDIT_AT = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"),
                        IS_CANCELED = "0"
                    });
                    //更新STATUS=3
                    _PACKAGE_BASE_INFO.UpdateStatus(PACKAGE_NO, VERSION_NO, FACTORY_ID, "3");
                }
            }
            return 1;
        }

        public Model.Package.Preview PostAuditInfo(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID)
        {
            var currentStep = GetCurrentPkgWfStep(PACKAGE_NO, VERSION_NO, FACTORY_ID);
            if (currentStep == null) return null;
            var nextStep = GetWfSetNextStep(PACKAGE_NO, VERSION_NO, FACTORY_ID);
            var users = GetAuditors(PACKAGE_NO, VERSION_NO, FACTORY_ID);

            Model.Package.Preview info = new Model.Package.Preview
            {
                CurrentStepName = currentStep.WF_SET_STEP_NAME,
                NextStepName = (nextStep == null ? "" : nextStep.WF_SET_STEP_NAME),
                Auditors = users,
                STEP_FLAG = (nextStep == null ? "LST" : nextStep.STEP_FLAG),
                IS_AUDITOR = CheckRight(PACKAGE_NO, VERSION_NO, FACTORY_ID),
                CURRENT_STEP_FLAG = currentStep.STEP_FLAG
            };
            return info;
        }

        //发送邮件
        public int SendAgreeEmail(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID, string[] TO, string[] CC)
        {
            var packageLst = _PACKAGE_BASE_INFO.GetDataById(PACKAGE_NO, FACTORY_ID, VERSION_NO, "");
            if (!packageLst.Any()) return 0;
            var package = packageLst.First();
            var authorLst = _Users.GetDataByUserNum(package.PREPARED_BY, "");
            string author = authorLst.Any() ? authorLst.First().CNNAME : "";
            var currentPkgWfStep = GetCurrentPkgWfStep(PACKAGE_NO, VERSION_NO, FACTORY_ID);
            if (currentPkgWfStep == null) return 0;

            string Subject = "Package文件签审---" + package.PACKAGE_NO + "-" + package.VERSION_NO;
            string Body = string.Empty;

            string host = HttpContext.Current.Request.Url.Authority;

            string str = GetTemplate("/Settings/EmailTemplate1.html");
            Body = string.Format(
                str,
                host,
                package.PACKAGE_NO,
                package.FACTORY_ID,
                package.VERSION_NO,
                package.PRODUCT_TYPE_ID,
                package.PRODUCT_PROC_TYPE_ID,
                package.PURPOSE,
                currentPkgWfStep.WF_SET_STEP_NAME,
                package.PREPARED_DATE,
                author,
                AuditHistory(PACKAGE_NO, VERSION_NO, FACTORY_ID)
                );

            using (var smtp = new SmtpClient())
            {
                var mail = new MailMessage();
                mail.Subject = Subject;
                mail.Body = Body;
                if (TO != null)
                {
                    foreach (var to in TO)
                    {
                        if (string.IsNullOrEmpty(to)) continue;
                        mail.To.Add(new MailAddress(to));
                    }
                }
                if (CC != null)
                {
                    foreach (var cc in CC)
                    {
                        if (string.IsNullOrEmpty(cc)) continue;
                        mail.CC.Add(new MailAddress(cc));
                    }
                }


                mail.IsBodyHtml = true;
                try
                {
                    smtp.Send(mail);
                }
                catch
                {
                }
            }
            return 1;
        }

        public string GetTemplate(string path)
        {
            path = HttpContext.Current.Server.MapPath(path);
            using (StreamReader sr = new StreamReader(path))
            {
                try
                {
                    string result = sr.ReadToEnd();
                    return result;
                }
                catch (Exception err)
                {
                    //throw new Exception(err.Message);
                    return "";
                }
            }
        }

        public string AuditHistory(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID)
        {
            var packageLst = _PACKAGE_BASE_INFO.GetDataById(PACKAGE_NO, FACTORY_ID, VERSION_NO, "");
            if (!packageLst.Any()) return "";
            var wfSetStepId = packageLst.First().APPROVE_FLOW_ID;
            var pkgWfStep = _PACKAGE_WF_STEP.GetDataByPkgId(PACKAGE_NO, VERSION_NO, FACTORY_ID);
            var sb = new StringBuilder();
            var lst = from x in pkgWfStep
                      orderby x.UPDATE_DATE descending
                      select x
                      ;
            foreach (var step in lst)
            {
                sb.AppendFormat("<span style=\"color:blue;font-size:12px;\">步骤：</span><span style=\"color:black;font-size:12px;\">{0}</span>&nbsp;", step.WF_SET_STEP_NAME);
                var pkgWfStepAuditor = _PACKAGE_WF_STEP_AUDITOR.GetDataByPkgStepId(step.PACKAGE_WF_STEP_ID).Where(x => x.IS_CANCELED != "1").ToList();
                foreach (var auditor in pkgWfStepAuditor)
                {
                    //sb.Append("<br />");
                    if (auditor.IS_AGREED == "1")
                        sb.AppendFormat("<span style=\"color:green;font-size:12px;\">【{0}】</span>&nbsp;<span style=\"color:gray;font-size:12px;\">{4}</span>&nbsp;<span style=\"color:black;font-size:12px;\">【{2}】</span>&nbsp;<span style=\"color:black;font-size:12px;\">{1}</span>&nbsp;<span style=\"color:black;font-size:12px;\">{3}</span>", "同&nbsp;&nbsp;意", auditor.CNNAME, auditor.TITLE, auditor.AUDIT_AT, auditor.AUDITOR_COMMENT);
                    else if (auditor.IS_AGREED == "2")
                    {
                        sb.AppendFormat("<span style=\"color:red;font-size:12px;\">【{0}】</span>&nbsp;<span style=\"color:gray;font-size:12px;\">{4}</span>&nbsp;<span style=\"color:black;font-size:12px;\">【{2}】</span>&nbsp;<span style=\"color:black;font-size:12px;\">{1}</span>&nbsp;<span style=\"color:black;font-size:12px;\">{3}</span>", "不同意", auditor.CNNAME, auditor.TITLE, auditor.AUDIT_AT, auditor.AUDITOR_COMMENT);
                    }
                    else if (auditor.IS_AGREED == "0")
                    {
                        sb.AppendFormat("<span style=\"color:orange;font-size:12px;\">【{0}】</span>&nbsp;<span style=\"color:gray;font-size:12px;\">{4}</span>&nbsp;<span style=\"color:black;font-size:12px;\">【{2}】</span>&nbsp;<span style=\"color:black;font-size:12px;\">{1}</span>&nbsp;<span style=\"color:black;font-size:12px;\">{3}</span>", "待处理", auditor.CNNAME, auditor.TITLE, "", auditor.AUDITOR_COMMENT);
                    }
                    sb.Append("<br />");
                }
                if (!pkgWfStepAuditor.Any()) sb.Append("<br />");
            }
            return sb.ToString();
        }

        public int CheckRight(string PACKAGE_NO, string VERSION_NO, string FACTORY_ID)
        {
            var currentUser = new Users().GetCurrentUser();
            //获取当前审批步骤 PACKAGE_WF_STEP
            var currentPkgWfStep = GetCurrentPkgWfStep(PACKAGE_NO, VERSION_NO, FACTORY_ID);
            if (currentPkgWfStep == null) return 0;
            //获取当前步骤的所有审批者
            var currentPkgWfStepAuditorNotCanceled = _PACKAGE_WF_STEP_AUDITOR.GetDataByPkgStepId(currentPkgWfStep.PACKAGE_WF_STEP_ID).Where(x => x.IS_CANCELED != "1" && x.PMES_USER_ID.Equals(currentUser.DESCRIPTION)).ToList();
            if (!currentPkgWfStepAuditorNotCanceled.Any()) return 0;
            return 1;
        }

        public List<Model.Settings.PMES_USER_GROUP_INFO_Entity> GetWfSetAuditorByPkgWfAuditorId(decimal AUDITOR_ID, string FACTORY_ID)
        {
            string group = _Preview.GetGroupIdByPkgWfStepAuditorId(AUDITOR_ID);
            return _PMES_USER_GROUP_INFO.GetDataByGroupId(group, FACTORY_ID, "");
        }

        public int ChangeAuditor(decimal AUDITOR_ID, string PMES_USER_ID)
        {
            var pkgWfStepAuditorLst = _PACKAGE_WF_STEP_AUDITOR.GetDataById(AUDITOR_ID, "");
            if (!pkgWfStepAuditorLst.Any()) return 0;
            var pkgWfStepAuditor = pkgWfStepAuditorLst.First();
            var pkgWfStepLst = _PACKAGE_WF_STEP.GetDataById(pkgWfStepAuditor.PACKAGE_WF_STEP_ID, "");
            if (!pkgWfStepLst.Any()) return 0;
            var pkgWfStep = pkgWfStepLst.First();

            #region 权限
            //当前步骤，是否是最后一步
            var currentPkgWfStep = GetCurrentPkgWfStep(pkgWfStep.PACKAGE_NO, pkgWfStep.VERSION_NO, pkgWfStep.FACTORY_ID);
            if (currentPkgWfStep == null) return 0;
            if (currentPkgWfStep.PACKAGE_WF_STEP_ID != pkgWfStep.PACKAGE_WF_STEP_ID) return 0;

            //取得上一步审核者，如果为当前用户，则可以修改。
            var previousStepList = _PACKAGE_WF_STEP.GetPrevious(pkgWfStep.PACKAGE_NO, pkgWfStep.VERSION_NO, pkgWfStep.FACTORY_ID, currentPkgWfStep.PACKAGE_WF_STEP_ID);
            if (!previousStepList.Any()) return 0;
            var previousStep = previousStepList.First();

            var previousAuditorList = _PACKAGE_WF_STEP_AUDITOR.GetDataByPkgStepId(previousStep.PACKAGE_WF_STEP_ID).Where(x => x.IS_CANCELED != "1" && x.IS_AGREED == "1");
            if (!previousAuditorList.Any()) return 0;
            var previousAuditor = previousAuditorList.First();
            var currentUser = new Users().GetCurrentUser();
            if (!previousAuditor.PMES_USER_ID.Equals(currentUser.DESCRIPTION)) return 0;


            #endregion

            //停用原来的AUDITOR_ID
            _PACKAGE_WF_STEP_AUDITOR.Update_IS_CANCELED(AUDITOR_ID);
            //新增新的 PACKAGE_WF_STEP_AUDITOR
            _PACKAGE_WF_STEP_AUDITOR.PostAdd(new PACKAGE_WF_STEP_AUDITOR_Entity
            {
                PACKAGE_WF_STEP_ID = pkgWfStepAuditor.PACKAGE_WF_STEP_ID,
                PMES_USER_ID = PMES_USER_ID,
                IS_AGREED = "0",
                IS_CANCELED = "0"
            });
            var user = _Users.GetDataByUserNum(PMES_USER_ID, "");
            SendAgreeEmail(pkgWfStep.PACKAGE_NO, pkgWfStep.VERSION_NO, pkgWfStep.FACTORY_ID, new[] { user.First().MAIL }, null);
            return 1;
        }
    }
}
