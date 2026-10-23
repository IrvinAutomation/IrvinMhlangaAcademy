using FluentAssertions.Execution;
using LAWTrust_API_Automation;
using LAWTrust_API_Automation.DataTransferModels;
using LAWTrust_API_Automation.MethodOperators;
using LAWTrust_API_Automation.RequestAndResponseHelpers;
using LAWTrust_APIAutomationTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using TestData;


namespace EnrolnmentTest
{
    [TestClass]
    public class SelfEnrolment
    {
        public PostOfficerLoginOperator _officerOperator;
        public GetProductsOperator _productsOperator;
        public PostSendOtpOperator _sendOtpOperator;
        public PostSelfEnrolmentOperator _selfEnrolmentOperator;
        public BaseTest _baseTest;

        public string getAccessToken;
        public Guid getClientId;
        public Guid getClientProductId;
        public Guid getOTPId;

        public SelfEnrolment()
        {
            _officerOperator = new PostOfficerLoginOperator();
            _productsOperator = new GetProductsOperator();
            _sendOtpOperator = new PostSendOtpOperator();
            _selfEnrolmentOperator = new PostSelfEnrolmentOperator();
            _baseTest = new BaseTest();
        }

        [TestMethod]
        public void VerifyThatAnApplicantCanEnrolThemselvesUsingTheSelfEnrolmentApplication()
        {
            //Arrange 
            var officerTestData = _baseTest.GetValidAltronPayload();
            var userOtpTestdata = _baseTest.SendOTPValidPayload();

            var officerResponse = _officerOperator.UpdateOfficerLogin(officerTestData);
            getAccessToken = officerResponse.Data.AccessToken;
            getClientId = officerResponse.Data.officer.ClientId;


            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();
            var clientsProductResponse = _productsOperator.GetProductsPerClient();

            SendOTPHelper.accessToken = getAccessToken;
            var userOtpResponse = _sendOtpOperator.PostSendUserOTP(userOtpTestdata);

            getClientProductId = clientsProductResponse.Data.Products[0].ClientProductId;
            getOTPId = userOtpResponse.Data.OtpId;

            SelfEnrolmentHelper.accessToken = getAccessToken;
            BaseTest.ClientId = getClientId.ToString();
            BaseTest.ClientProductId = getClientProductId.ToString();
            BaseTest.OTPId = getOTPId.ToString();

            var selfEnrolTestData = _baseTest.PostSelfEnrolmentValidpayload();

            //Act
            var selfEnrolmentResponse = _selfEnrolmentOperator.PostSelfEnrolmentUserDetails(selfEnrolTestData);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(selfEnrolmentResponse);
                Assert.AreEqual(true, selfEnrolmentResponse.Result.Success);
            }
        }

        [TestMethod]
        public void VerifyThatAnApplicantCanNotEnrolThemselvesUsingInvalidMobileNoInTheSelfEnrolmentApplication()
        {
            //Arrange 
            var officerTestData = _baseTest.GetValidAltronPayload();
            var userOtpTestdata = _baseTest.SendOTPValidPayload();

            var officerResponse = _officerOperator.UpdateOfficerLogin(officerTestData);
            getAccessToken = officerResponse.Data.AccessToken;
            getClientId = officerResponse.Data.officer.ClientId;


            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();
            var clientsProductResponse = _productsOperator.GetProductsPerClient();

            SendOTPHelper.accessToken = getAccessToken;
            var userOtpResponse = _sendOtpOperator.PostSendUserOTP(userOtpTestdata);

            getClientProductId = clientsProductResponse.Data.Products[0].ClientProductId;
            getOTPId = userOtpResponse.Data.OtpId;

            SelfEnrolmentHelper.accessToken = getAccessToken;
            BaseTest.ClientId = getClientId.ToString();
            BaseTest.ClientProductId = getClientProductId.ToString();
            BaseTest.OTPId = getOTPId.ToString();

            var selfEnrolTestData = _baseTest.PostSelfEnrolmentInValidpayload();

            //Act
            var selfEnrolmentResponse = _selfEnrolmentOperator.PostSelfEnrolmentUserDetails(selfEnrolTestData);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(selfEnrolmentResponse);
                Assert.AreEqual(false, selfEnrolmentResponse.Result.Success);
                Assert.AreEqual("Please ensure the Mobile Number has an Area Code.", selfEnrolmentResponse.Result.Error); ;
            }
        }
    }

    [TestClass]
    public class AutoApprovalEnrolment
    {
        public PostOfficerLoginOperator _officerOperator;
        public GetProductsOperator _productsOperator;
        public PostSendOtpOperator _sendOtpOperator;
        public PostAutoAppovalEnrolmentOperator _postAutoAppovalEnrolmentOperator;
        public BaseTest _baseTest;

        public string getAccessToken;
        public Guid getClientId;
        public Guid getClientProductId;
        public Guid getOTPId;

        public AutoApprovalEnrolment()
        {
            _officerOperator = new PostOfficerLoginOperator();
            _productsOperator = new GetProductsOperator();
            _sendOtpOperator = new PostSendOtpOperator();
            _postAutoAppovalEnrolmentOperator = new PostAutoAppovalEnrolmentOperator();
            _baseTest = new BaseTest();
        }

        [TestMethod]
        public void VerifyThatAnApplicantCanAutoEnrollThemselvesUsingTheAutoApprovalEnrolmentApplicationSuccessfully()
        {
            //Arrange 
            var officerPayload = _baseTest.GetValidAltronPayload();
            var userOtpPayload = _baseTest.SendOTPValidPayload();

            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);
            getAccessToken = officerResponse.Data.AccessToken;
            getClientId = officerResponse.Data.officer.ClientId;

            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();
            var clientsProductResponse = _productsOperator.GetProductsPerClient();

            SendOTPHelper.accessToken = getAccessToken;
            var userOtpResponse = _sendOtpOperator.PostSendUserOTP(userOtpPayload);

            getClientProductId = clientsProductResponse.Data.Products[1].ClientProductId;
            getOTPId = userOtpResponse.Data.OtpId;

            STCEnrolmentAutoApproveHelper.accessToken = getAccessToken;
            BaseTest.ClientId = getClientId.ToString();
            BaseTest.ClientProductId = getClientProductId.ToString();
            BaseTest.OTPId = getOTPId.ToString();

            var autoApprovalEnrolmentPayload = _baseTest.AutoApprovalEnrolmentValidpayload();

            // Act
            var autoApprovalEnrolmentResponse = _postAutoAppovalEnrolmentOperator.PostAutoApprovalEnrolmentUserDetails(autoApprovalEnrolmentPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(autoApprovalEnrolmentResponse);
                Assert.AreEqual(true, autoApprovalEnrolmentResponse.Result.Success);
            }
        }

        [TestMethod]
        public void VerifyThatAnApplicantCanNotAutoEnrolThemselvesUsingExistingEmailAddress()
        {
            //Arrange 
            var officerPayload = _baseTest.GetValidAltronPayload();
            var userOtpPayload = _baseTest.SendOTPValidPayload();

            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);
            getAccessToken = officerResponse.Data.AccessToken;
            getClientId = officerResponse.Data.officer.ClientId;

            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();
            var clientsProductResponse = _productsOperator.GetProductsPerClient();

            SendOTPHelper.accessToken = getAccessToken;
            var userOtpResponse = _sendOtpOperator.PostSendUserOTP(userOtpPayload);

            getClientProductId = clientsProductResponse.Data.Products[3].ClientProductId;
            getOTPId = userOtpResponse.Data.OtpId;

            STCEnrolmentAutoApproveHelper.accessToken = getAccessToken;
            BaseTest.ClientId = getClientId.ToString();
            BaseTest.ClientProductId = getClientProductId.ToString();
            BaseTest.OTPId = getOTPId.ToString();

            var autoApprovalEnrolmentPayload = _baseTest.AutoApprovalEnrolmentInValidpayload();

            // Act
            var autoApprovalEnrolmentResponse = _postAutoAppovalEnrolmentOperator.PostAutoApprovalEnrolmentUserDetails(autoApprovalEnrolmentPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(autoApprovalEnrolmentResponse);
                Assert.AreEqual(false, autoApprovalEnrolmentResponse.Result.Success);
                Assert.AreEqual("Applicant with the same email address already exists.", autoApprovalEnrolmentResponse.Result.Error);
            }
        }

        [TestMethod]
        public void VerifyThatAnApplicantCanNotAutoEnrollWithoutAClientId()
        {
            //Arrange 
            var officerPayload = _baseTest.GetValidAltronPayload();
            var userOtpPayload = _baseTest.SendOTPValidPayload();

            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);
            getAccessToken = officerResponse.Data.AccessToken;
            getClientId = officerResponse.Data.officer.ClientId;

            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();
            var clientsProductResponse = _productsOperator.GetProductsPerClient();

            SendOTPHelper.accessToken = getAccessToken;
            var userOtpResponse = _sendOtpOperator.PostSendUserOTP(userOtpPayload);

            getClientProductId = clientsProductResponse.Data.Products[1].ClientProductId;
            getOTPId = userOtpResponse.Data.OtpId;

            STCEnrolmentAutoApproveHelper.accessToken = getAccessToken;
            BaseTest.ClientId = "".ToString();
            BaseTest.ClientProductId = getClientProductId.ToString();
            BaseTest.OTPId = getOTPId.ToString();

            var autoApprovalEnrolmentPayload = _baseTest.AutoApprovalEnrolmentValidpayload();

            // Act
            var autoApprovalEnrolmentResponse = _postAutoAppovalEnrolmentOperator.PostAutoApprovalEnrolmentUserDetails(autoApprovalEnrolmentPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(autoApprovalEnrolmentResponse);
                Assert.AreEqual(false, autoApprovalEnrolmentResponse.Result.Success);
                Assert.AreEqual("Please provide ClientId.", autoApprovalEnrolmentResponse.Result.Error);
            }
        }

        [TestMethod]
        public void VerifyThatAnApplicantCanNotAutoEnrollWithoutAClientProductId()
        {
            //Arrange 
            var officerPayload = _baseTest.GetValidAltronPayload();
            var userOtpPayload = _baseTest.SendOTPValidPayload();

            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);
            getAccessToken = officerResponse.Data.AccessToken;
            getClientId = officerResponse.Data.officer.ClientId;

            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();
            var clientsProductResponse = _productsOperator.GetProductsPerClient();

            SendOTPHelper.accessToken = getAccessToken;
            var userOtpResponse = _sendOtpOperator.PostSendUserOTP(userOtpPayload);

            getClientProductId = clientsProductResponse.Data.Products[1].ClientProductId;
            getOTPId = userOtpResponse.Data.OtpId;

            STCEnrolmentAutoApproveHelper.accessToken = getAccessToken;
            BaseTest.ClientId = getClientId.ToString();
            BaseTest.ClientProductId = "".ToString();
            BaseTest.OTPId = getOTPId.ToString();

            var autoApprovalEnrolmentPayload = _baseTest.AutoApprovalEnrolmentValidpayload();

            // Act
            var autoApprovalEnrolmentResponse = _postAutoAppovalEnrolmentOperator.PostAutoApprovalEnrolmentUserDetails(autoApprovalEnrolmentPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(autoApprovalEnrolmentResponse);
                Assert.AreEqual(false, autoApprovalEnrolmentResponse.Result.Success);
                Assert.AreEqual("Please provide a ClientProductId.", autoApprovalEnrolmentResponse.Result.Error);
            }
        }

        [TestMethod]
        public void VerifyThatAnApplicantCanNotAutoEnrollWithoutOTPId()
        {
            //Arrange 
            var officerPayload = _baseTest.GetValidAltronPayload();
            var userOtpPayload = _baseTest.SendOTPValidPayload();

            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);
            getAccessToken = officerResponse.Data.AccessToken;
            getClientId = officerResponse.Data.officer.ClientId;

            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();
            var clientsProductResponse = _productsOperator.GetProductsPerClient();

            SendOTPHelper.accessToken = getAccessToken;
            var userOtpResponse = _sendOtpOperator.PostSendUserOTP(userOtpPayload);

            getClientProductId = clientsProductResponse.Data.Products[1].ClientProductId;
            getOTPId = userOtpResponse.Data.OtpId;

            STCEnrolmentAutoApproveHelper.accessToken = getAccessToken;
            BaseTest.ClientId = getClientId.ToString();
            BaseTest.ClientProductId = getClientProductId.ToString();
            BaseTest.OTPId = "".ToString();

            var autoApprovalEnrolmentPayload = _baseTest.AutoApprovalEnrolmentValidpayload();

            // Act
            var autoApprovalEnrolmentResponse = _postAutoAppovalEnrolmentOperator.PostAutoApprovalEnrolmentUserDetails(autoApprovalEnrolmentPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(autoApprovalEnrolmentResponse);
                Assert.AreEqual(false, autoApprovalEnrolmentResponse.Result.Success);
                Assert.AreEqual("Please provide an OTPId.", autoApprovalEnrolmentResponse.Result.Error);
            }
        }
    }

    [TestClass]
    public class AutoApproveSigningHubEnrolments
    {
        public PostOfficerLoginOperator _officerOperator;
        public PostSigningHubEnrolmentOperator _postSigningHubEnrolmentOperator;
        public GetProductsOperator _productsOperator;
        public BaseTest _baseTest;
        public string getAccessToken;
        public Guid getClientId;
        public Guid getClientProductId;

        public AutoApproveSigningHubEnrolments()
        {
            _officerOperator = new PostOfficerLoginOperator();
            _postSigningHubEnrolmentOperator = new PostSigningHubEnrolmentOperator();
            _productsOperator = new GetProductsOperator();
            _baseTest = new BaseTest();
        }

        [TestMethod]
        public void VerifyThatAnOfficerCanEnrolaUserForSigningHub07()
        {
            var officerPayload = _baseTest.GetValidAltronPayload();
            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);

            getAccessToken = officerResponse.Data.AccessToken;
          
            BaseTest.ClientId = "799371b9-8088-4245-a4f6-7ae09e27a365";
            BaseTest.ClientProductId = "20de80cf-0911-4eb7-97b7-67346d50fbbe";
            SigningHubEnrolmentHelper.accessToken = getAccessToken;

            var signingHubEnrolmentPayload = _baseTest.PostSigningHubEnrolmentValidpayload();

            //Act
            var signingHubEnrolmentResponse = _postSigningHubEnrolmentOperator.PostSigningHubEnrolmentUserDetails(signingHubEnrolmentPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(signingHubEnrolmentResponse);
                Assert.IsNotNull(signingHubEnrolmentResponse.Data.Person.ClientProductId);
                Assert.IsNotNull(signingHubEnrolmentResponse.Data.Person.ClientId);
                Assert.IsNotNull(signingHubEnrolmentResponse.Data.Person.PersonId);
                Assert.AreEqual(true, signingHubEnrolmentResponse.Result.Success);
            }
        }

        [TestMethod]
        public void VerifyThatAnOfficerCanEnrolaUserForSigningHub10()
        {
            var officerPayload = _baseTest.GetValidAltronPayload();
            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);

            getAccessToken = officerResponse.Data.AccessToken;
            getClientId = officerResponse.Data.officer.ClientId;

            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();
            var clientsProductResponse = _productsOperator.GetProductsPerClient();

            getClientProductId = clientsProductResponse.Data.Products[2].ClientProductId;

            BaseTest.ClientId = getClientId.ToString();
            BaseTest.ClientProductId = getClientProductId.ToString();
            SigningHubEnrolmentHelper.accessToken = getAccessToken;

            var signingHubEnrolmentPayload = _baseTest.PostSigningHubEnrolmentValidpayload();

            //Act
            var signingHubEnrolmentResponse = _postSigningHubEnrolmentOperator.PostSigningHubEnrolmentUserDetails(signingHubEnrolmentPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(signingHubEnrolmentResponse);
                Assert.IsNotNull(signingHubEnrolmentResponse.Data.Person.ClientProductId);
                Assert.IsNotNull(signingHubEnrolmentResponse.Data.Person.ClientId);
                Assert.IsNotNull(signingHubEnrolmentResponse.Data.Person.PersonId);
                Assert.AreEqual(true, signingHubEnrolmentResponse.Result.Success);
            }
        }

        [TestMethod]
        public void VerifyThatAnOfficerCannotEnrolaUserUsingExistingEmailAddressForSigningHub10()
        {
            //Arrange
            var officerPayload = _baseTest.GetValidAltronPayload();
            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);

            getClientId = officerResponse.Data.officer.ClientId;

            getAccessToken = officerResponse.Data.AccessToken;
           
            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();
            var clientsProductResponse = _productsOperator.GetProductsPerClient();

            getClientProductId = clientsProductResponse.Data.Products[2].ClientProductId;

            BaseTest.ClientId = getClientId.ToString();
            BaseTest.ClientProductId = getClientProductId.ToString();
            SigningHubEnrolmentHelper.accessToken = getAccessToken;

            var signingHubEnrolmentPayload = _baseTest.PostSigningHubEnrolmentInValidpayload();

            //Act
            var signingHubEnrolmentResponse = _postSigningHubEnrolmentOperator.PostSigningHubEnrolmentUserDetails(signingHubEnrolmentPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(signingHubEnrolmentResponse);
                Assert.AreEqual(false, signingHubEnrolmentResponse.Result.Success);
                Assert.AreEqual("Applicant with the same email address already exists.", signingHubEnrolmentResponse.Result.Error);
            }
        }

        [TestMethod]
        public void VerifyThatAnOfficerCannotEnrolaUserUsingaClientProductIdThatIsNotForSigningHub()
        {
            //Arrange
            var offierPayload = _baseTest.GetValidAltronPayload();
            var officerResponse = _officerOperator.UpdateOfficerLogin(offierPayload);

            getClientId = officerResponse.Data.officer.ClientId;
            getAccessToken = officerResponse.Data.AccessToken;

            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();
            var clientProductResponse = _productsOperator.GetProductsPerClient();

            getClientProductId = clientProductResponse.Data.Products[0].ClientProductId;

            BaseTest.ClientId = getClientId.ToString();
            BaseTest.ClientProductId = getClientProductId.ToString();
            SigningHubEnrolmentHelper.accessToken = getAccessToken;

            var signingHubEnrolmentPayload = _baseTest.PostSigningHubEnrolmentValidpayload();

            //Act
            var signingHubEnrolmentResponse = _postSigningHubEnrolmentOperator.PostSigningHubEnrolmentUserDetails(signingHubEnrolmentPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(signingHubEnrolmentResponse);
                Assert.AreEqual(false, signingHubEnrolmentResponse.Result.Success);
                Assert.AreEqual("This is not SigningHub enrolment.", signingHubEnrolmentResponse.Result.Error);
            }
        }

        [TestMethod]
        public void VerifyThatAnOfficerCannotEnrolaUserWithoutaClientId()
        {
            //Arrange
            var officerPayload = _baseTest.GetValidPayload();
            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);

            getClientId = officerResponse.Data.officer.ClientId;
            getAccessToken = officerResponse.Data.AccessToken;

            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();
            var clientProductResponse = _productsOperator.GetProductsPerClient();

            getClientProductId = clientProductResponse.Data.Products[2].ClientProductId;

            BaseTest.ClientId = "".ToString();
            BaseTest.ClientProductId = getClientProductId.ToString();
            SigningHubEnrolmentHelper.accessToken = getAccessToken;

            var signingHubEnrolmentPayload = _baseTest.PostSigningHubEnrolmentValidpayload();

            //Act
            var signingHubEnrolmentReponse = _postSigningHubEnrolmentOperator.PostSigningHubEnrolmentUserDetails(signingHubEnrolmentPayload);

            //Assert
            using (new AssertionScope())
            {

                Assert.IsNotNull(signingHubEnrolmentReponse);
                Assert.AreEqual(false, signingHubEnrolmentReponse.Result.Success);
                Assert.AreEqual("Please provide ClientId.", signingHubEnrolmentReponse.Result.Error);
            }
        }

        [TestMethod]
        public void VerifyThatAnOfficerCannotEnrolaUserWithoutaClientProductId()
        {
            //Arrange
            var officerPayload = _baseTest.GetValidAltronPayload();
            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);

            getClientId = officerResponse.Data.officer.ClientId;
            getAccessToken = officerResponse.Data.AccessToken;

            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();
            var clientProductResponse = _productsOperator.GetProductsPerClient();

            BaseTest.ClientId = getClientId.ToString();
            BaseTest.ClientProductId = "".ToString();
            SigningHubEnrolmentHelper.accessToken = getAccessToken;

            var signingHubEnrolmentPayload = _baseTest.PostSigningHubEnrolmentValidpayload();

            //Act
            var signingHubEnrolmentReponse = _postSigningHubEnrolmentOperator.PostSigningHubEnrolmentUserDetails(signingHubEnrolmentPayload);

            //Assert
            using (new AssertionScope())
            {

                Assert.IsNotNull(signingHubEnrolmentReponse);
                Assert.AreEqual(false, signingHubEnrolmentReponse.Result.Success);
                Assert.AreEqual("Please provide a ClientProductId.", signingHubEnrolmentReponse.Result.Error);
            }
        }
    }

    [TestClass]
    public class AgentPDO
    {
        public PostOfficerLoginOperator _officerOperator;
        public PostAgentPDOOperator _postPDOAgentOperator;
        public GetProductsOperator _productsOperator;
        public BaseTest _baseTest;
        public string getAccessToken;
        public Guid getClientId;
        public Guid getClientProductId1;
        public Guid getClientProductId2;
        public Guid getClientProductId3;
        public Guid[] getClientProductIds = new Guid[1];
        public string businessUnit;

        public AgentPDO()
        {
            _officerOperator = new PostOfficerLoginOperator();
            _postPDOAgentOperator = new PostAgentPDOOperator();
            _productsOperator = new GetProductsOperator();
            _baseTest = new BaseTest();
        }

        [TestMethod]
        public void VerifyThatAnOfficerCanEnrolaUserForRASusingPDOAgent()
        {
            var officerPayload = _baseTest.GetValidAltronPayload();

            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);
            getAccessToken = officerResponse.Data.AccessToken;
            getClientId = officerResponse.Data.officer.ClientId;

            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();
            var clientsProductResponse = _productsOperator.GetProductsPerClient();

            getClientProductId1 = clientsProductResponse.Data.Products[2].ClientProductId;

            getClientProductIds[0] = getClientProductId1;

            BaseTest.ClientId = getClientId.ToString();
            BaseTest.ClientProductIds = getClientProductIds;
            BaseTest.businessUnit = "HR department";
            AgentPDOHelper.accessToken = getAccessToken;

            var pdoAgentEnrolPayload = _baseTest.PostAgentPDOEnrolmentValidpayload();

            //Act
            var pdoAgentEnrolmentResponse = _postPDOAgentOperator.PostPdoAgentEnrolmentUserDetails(pdoAgentEnrolPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(pdoAgentEnrolmentResponse);
                Assert.AreEqual(true, pdoAgentEnrolmentResponse.Result.Success);
                Assert.IsNotNull(pdoAgentEnrolmentResponse.Data.Person.ClientProductIds);
            }
        }

        [TestMethod]
        public void VerifyThatAnOfficerCanEnrolaUserForADSSusingPDOAgent()
        {
            var officerPayload = _baseTest.GetValidAltronPayload();

            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);
            getAccessToken = officerResponse.Data.AccessToken;
            getClientId = officerResponse.Data.officer.ClientId;

            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();
            var clientsProductResponse = _productsOperator.GetProductsPerClient();

            getClientProductId1 = clientsProductResponse.Data.Products[0].ClientProductId;

            getClientProductIds[0] = getClientProductId1;

            BaseTest.ClientId = getClientId.ToString();
            BaseTest.ClientProductIds = getClientProductIds;
            BaseTest.businessUnit = "HR department";
            AgentPDOHelper.accessToken = getAccessToken;

            var pdoAgentEnrolPayload = _baseTest.PostAgentPDOEnrolmentValidpayload();

            //Act
            var pdoAgentEnrolmentResponse = _postPDOAgentOperator.PostPdoAgentEnrolmentUserDetails(pdoAgentEnrolPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(pdoAgentEnrolmentResponse);
                Assert.AreEqual(true, pdoAgentEnrolmentResponse.Result.Success);
                Assert.IsNotNull(pdoAgentEnrolmentResponse.Data.Person.ClientProductIds);
            }
        }

        [TestMethod]
        public void VerifyThatAnOfficerCannotEnrolUsingClientProductIdWithoutSettingsusingPDOAgent()
        {
            var officerPayload = _baseTest.GetValidAltronPayload();

            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);
            getAccessToken = officerResponse.Data.AccessToken;
            getClientId = officerResponse.Data.officer.ClientId;

            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();
            var clientsProductResponse = _productsOperator.GetProductsPerClient();

            getClientProductId1 = clientsProductResponse.Data.Products[5].ClientProductId;

            getClientProductIds[0] = getClientProductId1;

            BaseTest.ClientId = getClientId.ToString();
            BaseTest.ClientProductIds = getClientProductIds;
            BaseTest.businessUnit = "HR department";
            AgentPDOHelper.accessToken = getAccessToken;

            var pdoAgentEnrolPayload = _baseTest.PostAgentPDOEnrolmentValidpayload();

            //Act
            var pdoAgentEnrolmentResponse = _postPDOAgentOperator.PostPdoAgentEnrolmentUserDetails(pdoAgentEnrolPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(pdoAgentEnrolmentResponse);
                Assert.AreEqual(false, pdoAgentEnrolmentResponse.Result.Success);
                Assert.IsNotNull("Client Product <1229f02e-9194-4d1d-89dc-c9102d327495> Settings Not Found", pdoAgentEnrolmentResponse.Result.Error);
            }
        }

        [TestMethod]
        public void VerifyThatAnOfficerCanNotEnrolaUserUsingExistingEmailAddressOnPDOAgentApplication()
        {
            var officerPayload = _baseTest.GetValidAltronPayload();

            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);
            getAccessToken = officerResponse.Data.AccessToken;
            getClientId = officerResponse.Data.officer.ClientId;

            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();
            var clientsProductResponse = _productsOperator.GetProductsPerClient();

            getClientProductId1 = clientsProductResponse.Data.Products[0].ClientProductId;
            getClientProductId2 = clientsProductResponse.Data.Products[2].ClientProductId;

            getClientProductIds[0] = getClientProductId1;

            BaseTest.ClientId = getClientId.ToString();
            BaseTest.ClientProductIds = getClientProductIds;
            BaseTest.businessUnit = "HR department";
            AgentPDOHelper.accessToken = getAccessToken;

            var pdoAgentEnrolPayload = _baseTest.PostAgentPDOEnrolmentInValidpayload();

            //Act
            var pdoAgentEnrolmentResponse = _postPDOAgentOperator.PostPdoAgentEnrolmentUserDetails(pdoAgentEnrolPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(pdoAgentEnrolmentResponse);
                Assert.AreEqual(false, pdoAgentEnrolmentResponse.Result.Success);
                Assert.AreEqual("Applicant with the same email address already exists.", pdoAgentEnrolmentResponse.Result.Error);
            }
        }
    }

    [TestClass]
    public class SelfPDO
    {
        public PostOfficerLoginOperator _officerOperator;
        public GetProductsOperator _productsOperator;
        public PostSendOtpOperator _sendOtpOperator;
        public PostSelfPDOOperator _postSelfPDOOperator;
        public BaseTest _baseTest;
        public string getAccessToken;
        public Guid getClientId;
        public Guid getOTPId;
        public Guid getClientProductId1;
        public Guid getClientProductId2;
        public Guid getClientProductId3;
        public Guid[] getClientProductIds = new Guid[3];
        public SelfPDO()
        {
            _officerOperator = new PostOfficerLoginOperator();
            _productsOperator = new GetProductsOperator();
            _sendOtpOperator = new PostSendOtpOperator();
            _postSelfPDOOperator = new PostSelfPDOOperator();
            _baseTest = new BaseTest();
        }

        [TestMethod]
        public void VerifyThatAUsercanEnrolSuccessfullyUsingSElfPDOApplication()
        {
            //Arrange 
            var officerPayload = _baseTest.GetValidAltronPayload();
            var userOtpPayload = _baseTest.SendOTPValidPayload();

            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);
            getAccessToken = officerResponse.Data.AccessToken;
            getClientId = officerResponse.Data.officer.ClientId;

            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();
            var clientsProductResponse = _productsOperator.GetProductsPerClient();

            getClientProductId1 = clientsProductResponse.Data.Products[1].ClientProductId;
            getClientProductId2 = clientsProductResponse.Data.Products[2].ClientProductId;
            getClientProductId3 = clientsProductResponse.Data.Products[3].ClientProductId;

            getClientProductIds[0] = getClientProductId1;
            getClientProductIds[1] = getClientProductId2;
            getClientProductIds[2] = getClientProductId3;

            SendOTPHelper.accessToken = getAccessToken;
            var userOtpResponse = _sendOtpOperator.PostSendUserOTP(userOtpPayload);

            getOTPId = userOtpResponse.Data.OtpId;

            BaseTest.ClientId = getClientId.ToString();
            BaseTest.ClientProductIds = getClientProductIds;
            BaseTest.OTPId = getOTPId.ToString();
            BaseTest.businessUnit = "IT department";
            SelfPDOHelper.accessToken = getAccessToken;

            var selfPDOPayload = _baseTest.PostSelfPDOEnrolmentValidpayload();

            //Act
            var selfpdoResponse = _postSelfPDOOperator.PostSefPDOEnrolmentUserDetails(selfPDOPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(selfpdoResponse);
                Assert.AreEqual(true, selfpdoResponse.Result.Success);
            }
        }

        [TestMethod]
        public void VerifyThatAUserCanNotEnrolSuccessfullyUsingExistingEmailAddressOnSelfPDOApplication()
        {
            //Arrange 
            var officerPayload = _baseTest.GetValidAltronPayload();
            var userOtpPayload = _baseTest.SendOTPValidPayload();

            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);
            getAccessToken = officerResponse.Data.AccessToken;
            getClientId = officerResponse.Data.officer.ClientId;

            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();
            var clientsProductResponse = _productsOperator.GetProductsPerClient();

            getClientProductId1 = clientsProductResponse.Data.Products[0].ClientProductId;
            getClientProductId2 = clientsProductResponse.Data.Products[1].ClientProductId;
            getClientProductId3 = clientsProductResponse.Data.Products[3].ClientProductId;

            getClientProductIds[0] = getClientProductId1;
            getClientProductIds[1] = getClientProductId2;
            getClientProductIds[2] = getClientProductId3;

            SendOTPHelper.accessToken = getAccessToken;
            var userOtpResponse = _sendOtpOperator.PostSendUserOTP(userOtpPayload);

            getOTPId = userOtpResponse.Data.OtpId;

            BaseTest.ClientId = getClientId.ToString();
            BaseTest.ClientProductIds = getClientProductIds;
            BaseTest.OTPId = getOTPId.ToString();
            BaseTest.businessUnit = "IT department";
            SelfPDOHelper.accessToken = getAccessToken;

            var selfPDOPayload = _baseTest.PostSelfPDOEnrolmentInValidpayload();

            //Act
            var selfpdoResponse = _postSelfPDOOperator.PostSefPDOEnrolmentUserDetails(selfPDOPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(selfpdoResponse);
                Assert.AreEqual(false, selfpdoResponse.Result.Success);
                Assert.AreEqual("Applicant with the same email address already exists.", selfpdoResponse.Result.Error);
            }
        }
    }

    [TestClass]
    public class ADSSChangePassword
    {
        public PostOfficerLoginOperator _officerOperator;
        public ADSSChangePasswordOperator _adssChangePasswordOperator;
        public BaseTest _baseTest;
        public string getAccessToken;

        public ADSSChangePassword()
        {
            _officerOperator = new PostOfficerLoginOperator();
            _adssChangePasswordOperator = new ADSSChangePasswordOperator();
            _baseTest = new BaseTest();
        }

        [TestMethod]
        public void VerifyThatADSSPasswordCanBeChangedSuccessfully()
        {
            //Arrange 
            var officerPayload = _baseTest.GetValidAltronPayload();
            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);

            getAccessToken = officerResponse.Data.AccessToken;
            ADSSChangePasswordHelper.accessToken = getAccessToken;

            BaseTest.personId = "2cb4a6f4-5cbb-475c-a828-b25fdb105d5c";
            BaseTest.ClientProductId = "bf9b58db-c127-47ab-a2d9-1b885f96652a";
            BaseTest.password = "Password123!";
            BaseTest.newPassword = "Password123!";
            BaseTest.adssAlias = "85db03ea-73cd-4202-a655-860d1b152257";

            var adssChangePasswordPayload = _baseTest.ADSSChangePasswordPayload();

            //Act
            var adssChangePasswordResponse = _adssChangePasswordOperator.PostADSSChangePassword(adssChangePasswordPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(adssChangePasswordResponse);
                Assert.AreEqual("Certificate Password Changed Successfully!", adssChangePasswordResponse.Data.UserFriendlyMessage);
                Assert.AreEqual(true, adssChangePasswordResponse.Result.Success);
            }
        }

        [TestMethod]
        public void VerifyThatADSSPasswordCannotBeChangedIfTheAliasDoesNotExist()
        {
            //Arrange 
            var officerPayload = _baseTest.GetValidAltronPayload();
            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);

            getAccessToken = officerResponse.Data.AccessToken;
            ADSSChangePasswordHelper.accessToken = getAccessToken;

            BaseTest.personId = "2cb4a6f4-5cbb-475c-a828-b25fdb105d5c";
            BaseTest.ClientProductId = "bf9b58db-c127-47ab-a2d9-1b885f96652a";
            BaseTest.password = "Password123!";
            BaseTest.newPassword = "Password123!";
            BaseTest.adssAlias = "3TK359FB-EA51-480D-8376-C5BDD0D9D207";

            var adssChangePasswordPayload = _baseTest.ADSSChangePasswordPayload();

            //Act
            var adssChangePasswordResponse = _adssChangePasswordOperator.PostADSSChangePassword(adssChangePasswordPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(adssChangePasswordResponse);
                Assert.AreEqual("ADSS: Failed to change password for certificate [Error-43006] Failed to process request - certificate alias does not exist", adssChangePasswordResponse.Result.Error);
                Assert.AreEqual(false, adssChangePasswordResponse.Result.Success);
            }
        }

        [TestMethod]
        public void VerifyThatADSSPasswordCannotBeChangedIfOldPasswordIsInvalid()
        {
            //Arrange 
            var officerPayload = _baseTest.GetValidAltronPayload();
            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);

            getAccessToken = officerResponse.Data.AccessToken;
            ADSSChangePasswordHelper.accessToken = getAccessToken;

            BaseTest.personId = "2cb4a6f4-5cbb-475c-a828-b25fdb105d5c";
            BaseTest.ClientProductId = "bf9b58db-c127-47ab-a2d9-1b885f96652a";
            BaseTest.password = "Password123";
            BaseTest.newPassword = "Password123!";
            BaseTest.adssAlias = "85db03ea-73cd-4202-a655-860d1b152257";

            var adssChangePasswordPayload = _baseTest.ADSSChangePasswordPayload();

            //Act
            var adssChangePasswordResponse = _adssChangePasswordOperator.PostADSSChangePassword(adssChangePasswordPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(adssChangePasswordResponse);
                Assert.AreEqual("ADSS: Failed to change password for certificate [Error-43004] Failed to process request - the request contains invalid old password for PKCS#12/PFX", adssChangePasswordResponse.Result.Error);
                Assert.AreEqual(false, adssChangePasswordResponse.Result.Success);
            }
        }

        [TestMethod]
        public void VerifyThatADSSPasswordCannotBeChangedIfPersonIdIsInvalid()
        {
            //Arrange 
            var officerPayload = _baseTest.GetValidAltronPayload();
            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);

            getAccessToken = officerResponse.Data.AccessToken;
            ADSSChangePasswordHelper.accessToken = getAccessToken;

            BaseTest.personId = "2cb4a6f4-5cbb-475c-a828-b25fdb105d9d";
            BaseTest.ClientProductId = "bf9b58db-c127-47ab-a2d9-1b885f96652a";
            BaseTest.password = "Password123!";
            BaseTest.newPassword = "Password123!";
            BaseTest.adssAlias = "85db03ea-73cd-4202-a655-860d1b152257";

            var adssChangePasswordPayload = _baseTest.ADSSChangePasswordPayload();

            //Act
            var adssChangePasswordResponse = _adssChangePasswordOperator.PostADSSChangePassword(adssChangePasswordPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(adssChangePasswordResponse);
                Assert.AreEqual("Person Not Found", adssChangePasswordResponse.Result.Error);
                Assert.AreEqual(false, adssChangePasswordResponse.Result.Success);
            }
        }

        [TestMethod]
        public void VerifyThatADSSPasswordCannotBeChangedIfClientProductIdIsInvalid()
        {
            //Arrange 
            var officerPayload = _baseTest.GetValidAltronPayload();
            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);

            getAccessToken = officerResponse.Data.AccessToken;
            ADSSChangePasswordHelper.accessToken = getAccessToken;

            BaseTest.personId = "2cb4a6f4-5cbb-475c-a828-b25fdb105d5c";
            BaseTest.ClientProductId = "4849059D-418A-415C-8C89-5202916C3D7";
            BaseTest.password = "Password123!";
            BaseTest.newPassword = "Password123!";
            BaseTest.adssAlias = "85db03ea-73cd-4202-a655-860d1b152257";

            var adssChangePasswordPayload = _baseTest.ADSSChangePasswordPayload();

            //Act
            var adssChangePasswordResponse = _adssChangePasswordOperator.PostADSSChangePassword(adssChangePasswordPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(adssChangePasswordResponse);
                Assert.AreEqual("Please provide a ClientProductId.", adssChangePasswordResponse.Result.Error);
                Assert.AreEqual(false, adssChangePasswordResponse.Result.Success);
            }
        }

        [TestMethod]
        public void VerifyThatADSSPasswordCannotBeChangedIfNewPasswordIsNull()
        {
            //Arrange 
            var officerPayload = _baseTest.GetValidAltronPayload();
            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);

            getAccessToken = officerResponse.Data.AccessToken;
            ADSSChangePasswordHelper.accessToken = getAccessToken;

            BaseTest.personId = "2cb4a6f4-5cbb-475c-a828-b25fdb105d5c";
            BaseTest.ClientProductId = "bf9b58db-c127-47ab-a2d9-1b885f96652a";
            BaseTest.password = "Password123!";
            BaseTest.newPassword = null;
            BaseTest.adssAlias = "85db03ea-73cd-4202-a655-860d1b152257";

            var adssChangePasswordPayload = _baseTest.ADSSChangePasswordPayload();

            //Act
            var adssChangePasswordResponse = _adssChangePasswordOperator.PostADSSChangePassword(adssChangePasswordPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(adssChangePasswordResponse);
                Assert.AreEqual("Please provide the new certificate password.", adssChangePasswordResponse.Result.Error);
                Assert.AreEqual(false, adssChangePasswordResponse.Result.Success);
            }

        }
    }
    [TestClass]
    public class NoscotecEnrolments
    {
        public PostOfficerLoginOperator _officerOperator;
        public PostNoscotecEnrolmentsOperator _postNoscotecEnrolmentOperator;
        public GetProductsOperator _productsOperator;
        public BaseTest _baseTest;

        public string getAccessToken;
        public Guid getClientId;
        public Guid getClientProductId;

        public NoscotecEnrolments()
        {
            _officerOperator = new PostOfficerLoginOperator();
            _postNoscotecEnrolmentOperator = new PostNoscotecEnrolmentsOperator();
            _productsOperator = new GetProductsOperator();
            _baseTest = new BaseTest();
        }

        [TestMethod]
        public void VerifyThatAnOfficerCanEnrolaUserUsingNoscotecApplication()
        {
            var officerPayload = _baseTest.GetValidAltronPayload();

            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);
            getAccessToken = officerResponse.Data.AccessToken;
            getClientId = officerResponse.Data.officer.ClientId;

            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();
            var clientsProductResponse = _productsOperator.GetProductsPerClient();

            getClientProductId = clientsProductResponse.Data.Products[3].ClientProductId;

            BaseTest.ClientId = getClientId.ToString();
            BaseTest.ClientProductId = getClientProductId.ToString();
            NoscotecEnrolmentsHelper.accessToken = getAccessToken;

            var noscotecEnrolmentsPayload = _baseTest.NoscotecEnrolmentValidpayload();

            //Act
            var noscotecEnrolmentsResponse = _postNoscotecEnrolmentOperator.PostNoscotecEnrolmentUserDetails(noscotecEnrolmentsPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(noscotecEnrolmentsResponse);
                Assert.AreEqual(true, noscotecEnrolmentsResponse.Result.Success);
            }
        }

        [TestMethod]
        public void VerifyThatAnOfficerCanNotEnrolaUserUsingExistingEmailAddessOnNoscotecApplication()
        {
            var officerPayload = _baseTest.GetValidAltronPayload();

            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);
            getAccessToken = officerResponse.Data.AccessToken;
            getClientId = officerResponse.Data.officer.ClientId;

            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();
            var clientsProductResponse = _productsOperator.GetProductsPerClient();

            getClientProductId = clientsProductResponse.Data.Products[0].ClientProductId;

            BaseTest.ClientId = getClientId.ToString();
            BaseTest.ClientProductId = getClientProductId.ToString();
            NoscotecEnrolmentsHelper.accessToken = getAccessToken;

            var noscotecEnrolmentsPayload = _baseTest.NoscotecEnrolmentInValidpayload();

            //Act
            var noscotecEnrolmentsResponse = _postNoscotecEnrolmentOperator.PostNoscotecEnrolmentUserDetails(noscotecEnrolmentsPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(noscotecEnrolmentsResponse);
                Assert.AreEqual(false, noscotecEnrolmentsResponse.Result.Success);
                Assert.AreEqual("Applicant with the same email address already exists.", noscotecEnrolmentsResponse.Result.Error);
            }
        }
    }

    [TestClass]
    public class RegisterSigningHubElectronicUser
    {
        public PostOfficerLoginOperator _officerOperator;
        public PostRegisterSigningHubElectronicUserOperator _postRegisterSigningHubElectronicUserOperator;
        public BaseTest _baseTest;

        public string getAccessToken;
        public string getEmailAddress;

        public RegisterSigningHubElectronicUser()
        {
            _officerOperator = new PostOfficerLoginOperator();
            _postRegisterSigningHubElectronicUserOperator = new PostRegisterSigningHubElectronicUserOperator();
            _baseTest = new BaseTest();
        }

        [TestMethod]
        public void VerifyThatUserCanBeRegisteredElectronicallyForSigningHubSuccessfully()
        {
            //Arrage
            var officerPayload = _baseTest.GetValidAltronPayload();
            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);

            getAccessToken = officerResponse.Data.AccessToken;
            RegisterSigningHubElectronicUserHelper.accessToken = getAccessToken;

            var TestDataGenerator = new TestDataGenerator();
            String firstName = TestDataGenerator.TestNames(7);
            String lastName = TestDataGenerator.TestNames(8);

            BaseTest.userName = $"{firstName} {lastName}".ToString();
            BaseTest.emailAddress = $"{firstName}.{lastName}@altronAutomation.com".ToString();
            BaseTest.jobTitle = "Senior Consultant Testing".ToString();
            BaseTest.companyName = "Altron Security".ToString();
            BaseTest.mobileNumber = "+27832333896".ToString();
            BaseTest.identityNumber = "8806307583080".ToString();
            BaseTest.ClientProductId = "20de80cf-0911-4eb7-97b7-67346d50fbbe".ToString();

            var registerSigningHubElectronicUserPayload = _baseTest.RegisterSigningHubElectronicUserPayload();

            //Act
            var registerSigningHubElectronicUserResponse = _postRegisterSigningHubElectronicUserOperator.PostRegisterSigningHubElectronicUserDetails(registerSigningHubElectronicUserPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(registerSigningHubElectronicUserResponse);
                Assert.AreEqual(true, registerSigningHubElectronicUserResponse.Result.Success);
                Assert.AreEqual(true, registerSigningHubElectronicUserResponse.Data.Status);
                Assert.AreEqual("SigningHub account successfully registered", registerSigningHubElectronicUserResponse.Data.Message);
            }
        }

        [TestMethod]
        public void VerifyThatUserCannotBeRegisteredElectronicallyForSigningHubIfTheyAlreadyExisting()
        {
            //Arrage
            var officerPayload = _baseTest.GetValidAltronPayload();
            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);

            getAccessToken = officerResponse.Data.AccessToken;
            RegisterSigningHubElectronicUserHelper.accessToken = getAccessToken;

            var TestDataGenerator = new TestDataGenerator();
            String firstName = TestDataGenerator.TestNames(7);
            String lastName = TestDataGenerator.TestNames(8);

            BaseTest.userName = $"{firstName} {lastName}".ToString();
            BaseTest.emailAddress = $"{firstName}.{lastName}@altronAutomation.com".ToString();
            BaseTest.jobTitle = "Senior Consultant Testing".ToString();
            BaseTest.companyName = "Altron Security".ToString();
            BaseTest.mobileNumber = "+27832333896".ToString();
            BaseTest.identityNumber = "8806307583080".ToString();
            BaseTest.ClientProductId = "20de80cf-0911-4eb7-97b7-67346d50fbbe".ToString();

            var registerSigningHubElectronicUserPayload = _baseTest.RegisterSigningHubElectronicUserPayload();
            var registerSigningHubElectronicUserResponseNew = _postRegisterSigningHubElectronicUserOperator.PostRegisterSigningHubElectronicUserDetails(registerSigningHubElectronicUserPayload);

            getEmailAddress = registerSigningHubElectronicUserResponseNew.Data.EmailAddress.ToString();

            BaseTest.userName = $"{firstName} {lastName}".ToString();
            BaseTest.emailAddress = getEmailAddress;
            BaseTest.jobTitle = "Senior Consultant Testing".ToString();
            BaseTest.companyName = "Altron Security".ToString();
            BaseTest.mobileNumber = "+27832333896".ToString();
            BaseTest.identityNumber = "8806307583080".ToString();
            BaseTest.ClientProductId = "4b02d56f-abd6-4f49-a8ae-f72e3b3d33c2".ToString();

            var registerSigningHubElectronicUserPayloadSecond = _baseTest.RegisterSigningHubElectronicUserPayload();

            //Act
            var registerSigningHubElectronicUserResponseSecond = _postRegisterSigningHubElectronicUserOperator.PostRegisterSigningHubElectronicUserDetails(registerSigningHubElectronicUserPayloadSecond);
            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(registerSigningHubElectronicUserResponseSecond);
                Assert.AreEqual(false, registerSigningHubElectronicUserResponseSecond.Result.Success);
                Assert.AreEqual($"Signing Hub : SigningHub account {getEmailAddress} already exist", registerSigningHubElectronicUserResponseSecond.Result.Error);
            }
        }

        [TestMethod]
        public void VerifyThatUserCannotRegisterWithoutUsername()
        {
            //Arrage
            var officerPayload = _baseTest.GetValidAltronPayload();
            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);

            getAccessToken = officerResponse.Data.AccessToken;
            RegisterSigningHubElectronicUserHelper.accessToken = getAccessToken;

            var TestDataGenerator = new TestDataGenerator();
            String firstName = TestDataGenerator.TestNames(7);
            String lastName = TestDataGenerator.TestNames(8);

            BaseTest.userName = "".ToString();
            BaseTest.emailAddress = $"{firstName}.{lastName}@altronAutomation.com".ToString();
            BaseTest.jobTitle = "Senior Consultant Testing".ToString();
            BaseTest.companyName = "Altron Security".ToString();
            BaseTest.mobileNumber = "+27832333896".ToString();
            BaseTest.identityNumber = "8806307583080".ToString();
            BaseTest.ClientProductId = "20de80cf-0911-4eb7-97b7-67346d50fbbe".ToString();

            var registerSigningHubElectronicUserPayload = _baseTest.RegisterSigningHubElectronicUserPayload();

            //Act
            var registerSigningHubElectronicUserResponse = _postRegisterSigningHubElectronicUserOperator.PostRegisterSigningHubElectronicUserDetails(registerSigningHubElectronicUserPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(registerSigningHubElectronicUserResponse);
                Assert.AreEqual(false, registerSigningHubElectronicUserResponse.Result.Success);
                Assert.AreEqual("Please provide a Username.", registerSigningHubElectronicUserResponse.Result.Error);
            }
        }

        [TestMethod]
        public void VerifyThatUserCannotRegisterWithoutUserEmail()
        {
            //Arrage
            var officerPayload = _baseTest.GetValidAltronPayload();
            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);

            getAccessToken = officerResponse.Data.AccessToken;
            RegisterSigningHubElectronicUserHelper.accessToken = getAccessToken;

            var TestDataGenerator = new TestDataGenerator();
            String firstName = TestDataGenerator.TestNames(7);
            String lastName = TestDataGenerator.TestNames(8);

            BaseTest.userName = $"{firstName} {lastName}".ToString();
            BaseTest.emailAddress = "".ToString();
            BaseTest.jobTitle = "Senior Consultant Testing".ToString();
            BaseTest.companyName = "Altron Security".ToString();
            BaseTest.mobileNumber = "+27832333896".ToString();
            BaseTest.identityNumber = "8806307583080".ToString();
            BaseTest.ClientProductId = "20de80cf-0911-4eb7-97b7-67346d50fbbe".ToString();

            var registerSigningHubElectronicUserPayload = _baseTest.RegisterSigningHubElectronicUserPayload();

            //Act
            var registerSigningHubElectronicUserResponse = _postRegisterSigningHubElectronicUserOperator.PostRegisterSigningHubElectronicUserDetails(registerSigningHubElectronicUserPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(registerSigningHubElectronicUserResponse);
                Assert.AreEqual(false, registerSigningHubElectronicUserResponse.Result.Success);
                Assert.AreEqual("Please provide a Email Address.", registerSigningHubElectronicUserResponse.Result.Error);
            }
        }

        [TestMethod]
        public void VerifyThatUserCannotRegisterWithInvalidUserEmail()
        {
            //Arrage
            var officerPayload = _baseTest.GetValidAltronPayload();
            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);

            getAccessToken = officerResponse.Data.AccessToken;
            RegisterSigningHubElectronicUserHelper.accessToken = getAccessToken;

            var TestDataGenerator = new TestDataGenerator();
            String firstName = TestDataGenerator.TestNames(7);
            String lastName = TestDataGenerator.TestNames(8);

            BaseTest.userName = $"{firstName} {lastName}".ToString();
            BaseTest.emailAddress = "heveaerac".ToString();
            BaseTest.jobTitle = "Senior Consultant Testing".ToString();
            BaseTest.companyName = "Altron Security".ToString();
            BaseTest.mobileNumber = "+27832333896".ToString();
            BaseTest.identityNumber = "8806307583080".ToString();
            BaseTest.ClientProductId = "20de80cf-0911-4eb7-97b7-67346d50fbbe".ToString();

            var registerSigningHubElectronicUserPayload = _baseTest.RegisterSigningHubElectronicUserPayload();

            //Act
            var registerSigningHubElectronicUserResponse = _postRegisterSigningHubElectronicUserOperator.PostRegisterSigningHubElectronicUserDetails(registerSigningHubElectronicUserPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(registerSigningHubElectronicUserResponse);
                Assert.AreEqual(false, registerSigningHubElectronicUserResponse.Result.Success);
                Assert.AreEqual("Please provide valid Email Address.", registerSigningHubElectronicUserResponse.Result.Error);
            }
        }

        [TestMethod]
        public void VerifyThatUserCanRegisterWithoutaJobTitle()
        {
            //Arrage
            var officerPayload = _baseTest.GetValidAltronPayload();
            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);

            getAccessToken = officerResponse.Data.AccessToken;
            RegisterSigningHubElectronicUserHelper.accessToken = getAccessToken;

            var TestDataGenerator = new TestDataGenerator();
            String firstName = TestDataGenerator.TestNames(7);
            String lastName = TestDataGenerator.TestNames(8);

            BaseTest.userName = $"{firstName} {lastName}".ToString();
            BaseTest.emailAddress = $"{firstName}.{lastName}@altronAutomation.com".ToString();
            BaseTest.jobTitle = "".ToString();
            BaseTest.companyName = "Altron Security".ToString();
            BaseTest.mobileNumber = "+27832333896".ToString();
            BaseTest.identityNumber = "8806307583080".ToString();
            BaseTest.ClientProductId = "20de80cf-0911-4eb7-97b7-67346d50fbbe".ToString();

            var registerSigningHubElectronicUserPayload = _baseTest.RegisterSigningHubElectronicUserPayload();

            //Act
            var registerSigningHubElectronicUserResponse = _postRegisterSigningHubElectronicUserOperator.PostRegisterSigningHubElectronicUserDetails(registerSigningHubElectronicUserPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(registerSigningHubElectronicUserResponse);
                Assert.AreEqual(true, registerSigningHubElectronicUserResponse.Result.Success);
                Assert.AreEqual(true, registerSigningHubElectronicUserResponse.Data.Status);
                Assert.AreEqual("SigningHub account successfully registered", registerSigningHubElectronicUserResponse.Data.Message);
            }
        }

        [TestMethod]
        public void VerifyThatUserCanRegisterWithoutaCompanyName()
        {
            //Arrage
            var officerPayload = _baseTest.GetValidAltronPayload();
            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);

            getAccessToken = officerResponse.Data.AccessToken;
            RegisterSigningHubElectronicUserHelper.accessToken = getAccessToken;

            var TestDataGenerator = new TestDataGenerator();
            String firstName = TestDataGenerator.TestNames(7);
            String lastName = TestDataGenerator.TestNames(8);

            BaseTest.userName = $"{firstName} {lastName}".ToString();
            BaseTest.emailAddress = $"{firstName}.{lastName}@altronAutomation.com".ToString();
            BaseTest.jobTitle = "Senior Consultant Testing".ToString();
            BaseTest.companyName = "".ToString();
            BaseTest.mobileNumber = "+27832333896".ToString();
            BaseTest.identityNumber = "8806307583080".ToString();
            BaseTest.ClientProductId = "20de80cf-0911-4eb7-97b7-67346d50fbbe".ToString();

            var registerSigningHubElectronicUserPayload = _baseTest.RegisterSigningHubElectronicUserPayload();

            //Act
            var registerSigningHubElectronicUserResponse = _postRegisterSigningHubElectronicUserOperator.PostRegisterSigningHubElectronicUserDetails(registerSigningHubElectronicUserPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(registerSigningHubElectronicUserResponse);
                Assert.AreEqual(true, registerSigningHubElectronicUserResponse.Result.Success);
                Assert.AreEqual(true, registerSigningHubElectronicUserResponse.Data.Status);
                Assert.AreEqual("SigningHub account successfully registered", registerSigningHubElectronicUserResponse.Data.Message);
            }
        }

        [TestMethod]
        public void VerifyThatUserCannotRegisterIfMobileNumberHasNoAreaCode()
        {
            //Arrage
            var officerPayload = _baseTest.GetValidAltronPayload();
            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);

            getAccessToken = officerResponse.Data.AccessToken;
            RegisterSigningHubElectronicUserHelper.accessToken = getAccessToken;

            var TestDataGenerator = new TestDataGenerator();
            String firstName = TestDataGenerator.TestNames(7);
            String lastName = TestDataGenerator.TestNames(8);

            BaseTest.userName = $"{firstName} {lastName}".ToString();
            BaseTest.emailAddress = $"{firstName}.{lastName}@altronAutomation.com".ToString();
            BaseTest.jobTitle = "Senior Consultant Testing".ToString();
            BaseTest.companyName = "Altron Security".ToString();
            BaseTest.mobileNumber = "0832333896".ToString();
            BaseTest.identityNumber = "8806307583080".ToString();
            BaseTest.ClientProductId = "20de80cf-0911-4eb7-97b7-67346d50fbbe".ToString();

            var registerSigningHubElectronicUserPayload = _baseTest.RegisterSigningHubElectronicUserPayload();

            //Act
            var registerSigningHubElectronicUserResponse = _postRegisterSigningHubElectronicUserOperator.PostRegisterSigningHubElectronicUserDetails(registerSigningHubElectronicUserPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(registerSigningHubElectronicUserResponse);
                Assert.AreEqual(false, registerSigningHubElectronicUserResponse.Result.Success);
                Assert.AreEqual("Please ensure the Mobile Number has an Area Code.", registerSigningHubElectronicUserResponse.Result.Error);
            }
        }

        [TestMethod]
        public void VerifyThatUserCanRegisterWithoutMobileNumber()
        {
            //Arrage
            var officerPayload = _baseTest.GetValidAltronPayload();
            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);

            getAccessToken = officerResponse.Data.AccessToken;
            RegisterSigningHubElectronicUserHelper.accessToken = getAccessToken;

            var TestDataGenerator = new TestDataGenerator();
            String firstName = TestDataGenerator.TestNames(7);
            String lastName = TestDataGenerator.TestNames(8);

            BaseTest.userName = $"{firstName} {lastName}".ToString();
            BaseTest.emailAddress = $"{firstName}.{lastName}@altronAutomation.com".ToString();
            BaseTest.jobTitle = "Senior Consultant Testing".ToString();
            BaseTest.companyName = "Altron Security".ToString();
            BaseTest.mobileNumber = "".ToString();
            BaseTest.identityNumber = "8806307583080".ToString();
            BaseTest.ClientProductId = "20de80cf-0911-4eb7-97b7-67346d50fbbe".ToString();

            var registerSigningHubElectronicUserPayload = _baseTest.RegisterSigningHubElectronicUserPayload();

            //Act
            var registerSigningHubElectronicUserResponse = _postRegisterSigningHubElectronicUserOperator.PostRegisterSigningHubElectronicUserDetails(registerSigningHubElectronicUserPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(registerSigningHubElectronicUserResponse);
                Assert.AreEqual(true, registerSigningHubElectronicUserResponse.Result.Success);
                Assert.AreEqual(true, registerSigningHubElectronicUserResponse.Data.Status);
                Assert.AreEqual("SigningHub account successfully registered", registerSigningHubElectronicUserResponse.Data.Message);
            }
        }

        [TestMethod]
        public void VerifyThatUserCanRegisterWithoutIdentityNumber()
        {
            //Arrage
            var officerPayload = _baseTest.GetValidAltronPayload();
            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);

            getAccessToken = officerResponse.Data.AccessToken;
            RegisterSigningHubElectronicUserHelper.accessToken = getAccessToken;

            var TestDataGenerator = new TestDataGenerator();
            String firstName = TestDataGenerator.TestNames(7);
            String lastName = TestDataGenerator.TestNames(8);

            BaseTest.userName = $"{firstName} {lastName}".ToString();
            BaseTest.emailAddress = $"{firstName}.{lastName}@altronAutomation.com".ToString();
            BaseTest.jobTitle = "Senior Consultant Testing".ToString();
            BaseTest.companyName = "Altron Security".ToString();
            BaseTest.mobileNumber = "+27832888896".ToString();
            BaseTest.identityNumber = "".ToString();
            BaseTest.ClientProductId = "20de80cf-0911-4eb7-97b7-67346d50fbbe".ToString();

            var registerSigningHubElectronicUserPayload = _baseTest.RegisterSigningHubElectronicUserPayload();

            //Act
            var registerSigningHubElectronicUserResponse = _postRegisterSigningHubElectronicUserOperator.PostRegisterSigningHubElectronicUserDetails(registerSigningHubElectronicUserPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(registerSigningHubElectronicUserResponse);
                Assert.AreEqual(true, registerSigningHubElectronicUserResponse.Result.Success);
                Assert.AreEqual(true, registerSigningHubElectronicUserResponse.Data.Status);
                Assert.AreEqual("SigningHub account successfully registered", registerSigningHubElectronicUserResponse.Data.Message);
            }
        }

        [TestMethod]
        public void VerifyThatUserCannotRegisterWithInvalidClientProductId()
        {
            //Arrage
            var officerPayload = _baseTest.GetValidAltronPayload();
            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);

            getAccessToken = officerResponse.Data.AccessToken;
            RegisterSigningHubElectronicUserHelper.accessToken = getAccessToken;

            var TestDataGenerator = new TestDataGenerator();
            String firstName = TestDataGenerator.TestNames(7);
            String lastName = TestDataGenerator.TestNames(8);

            BaseTest.userName = $"{firstName} {lastName}".ToString();
            BaseTest.emailAddress = $"{firstName}.{lastName}@altronAutomation.com".ToString();
            BaseTest.jobTitle = "Senior Consultant Testing".ToString();
            BaseTest.companyName = "Altron Security".ToString();
            BaseTest.mobileNumber = "+27832888896".ToString();
            BaseTest.identityNumber = "8804195444083".ToString();
            BaseTest.ClientProductId = "4b02d56f-abd6-4f49-a8ae-f72e3b3d33c210".ToString();

            var registerSigningHubElectronicUserPayload = _baseTest.RegisterSigningHubElectronicUserPayload();

            //Act
            var registerSigningHubElectronicUserResponse = _postRegisterSigningHubElectronicUserOperator.PostRegisterSigningHubElectronicUserDetails(registerSigningHubElectronicUserPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(registerSigningHubElectronicUserResponse);
                Assert.AreEqual(false, registerSigningHubElectronicUserResponse.Result.Success);
                Assert.AreEqual("Please provide ClientProductId.", registerSigningHubElectronicUserResponse.Result.Error);
            }
        }

        [TestMethod]
        public void VerifyThatUserCannotRegisterWithClientProductIdWithNoSettings()
        {
            //Arrage
            var officerPayload = _baseTest.GetValidAltronPayload();
            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);

            getAccessToken = officerResponse.Data.AccessToken;
            RegisterSigningHubElectronicUserHelper.accessToken = getAccessToken;

            var TestDataGenerator = new TestDataGenerator();
            String firstName = TestDataGenerator.TestNames(7);
            String lastName = TestDataGenerator.TestNames(8);

            BaseTest.userName = $"{firstName} {lastName}".ToString();
            BaseTest.emailAddress = $"{firstName}.{lastName}@altronAutomation.com".ToString();
            BaseTest.jobTitle = "Senior Consultant Testing".ToString();
            BaseTest.companyName = "Altron Security".ToString();
            BaseTest.mobileNumber = "+27832888896".ToString();
            BaseTest.identityNumber = "8804195444083".ToString();
            BaseTest.ClientProductId = "1229f02e-9194-4d1d-89dc-c9102d327495".ToString();

            var registerSigningHubElectronicUserPayload = _baseTest.RegisterSigningHubElectronicUserPayload();

            //Act
            var registerSigningHubElectronicUserResponse = _postRegisterSigningHubElectronicUserOperator.PostRegisterSigningHubElectronicUserDetails(registerSigningHubElectronicUserPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(registerSigningHubElectronicUserResponse);
                Assert.AreEqual(false, registerSigningHubElectronicUserResponse.Result.Success);
                Assert.AreEqual("Client Product <1229f02e-9194-4d1d-89dc-c9102d327495> Settings Not Found", registerSigningHubElectronicUserResponse.Result.Error);
            }
        }
    }

    [TestClass]
    public class GetAdssDetails
    {
        public PostOfficerLoginOperator _officerOperator;
        public GetAdssDetailsOperator _getAdssDetailsOperator;
        public PostSendOtpOperator _sendOtpOperator;
        public PostAutoAppovalEnrolmentOperator _postAutoAppovalEnrolmentOperator;
        public GetProductsOperator _productsOperator;
        public GetAdssAliasOperator _getAdssAliasOperator;
        public BaseTest _baseTest;

        public string getAccessToken;
        public Guid getClientId;
        public Guid getClientProductId;
        public Guid getOTPId;

        public GetAdssDetails()
        {
            _officerOperator = new PostOfficerLoginOperator();
            _productsOperator = new GetProductsOperator();
            _sendOtpOperator = new PostSendOtpOperator();
            _postAutoAppovalEnrolmentOperator = new PostAutoAppovalEnrolmentOperator();
            _getAdssDetailsOperator = new GetAdssDetailsOperator();
            _getAdssAliasOperator = new GetAdssAliasOperator();
            _baseTest = new BaseTest();
        }

        [TestMethod]
        public void VerifyThatUserCanGetValidAddsDetailsByUsingValidClientProductIdAndValidAdssAlias()
        {
            //Arrange
            var officerPayload = _baseTest.GetValidAltronPayload();
            var userOtpPayload = _baseTest.SendOTPValidPayload();

            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);
            getAccessToken = officerResponse.Data.AccessToken;
            getClientId = officerResponse.Data.officer.ClientId;

            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();
            var clientsProductResponse = _productsOperator.GetProductsPerClient();

            SendOTPHelper.accessToken = getAccessToken;
            var userOtpResponse = _sendOtpOperator.PostSendUserOTP(userOtpPayload);

            getClientProductId = clientsProductResponse.Data.Products[0].ClientProductId;
            getOTPId = userOtpResponse.Data.OtpId;

            STCEnrolmentAutoApproveHelper.accessToken = getAccessToken;
            BaseTest.ClientId = getClientId.ToString();
            BaseTest.ClientProductId = getClientProductId.ToString();
            BaseTest.OTPId = getOTPId.ToString();

            var autoApprovalEnrolmentPayload = _baseTest.AutoApprovalEnrolmentValidpayload();
            var autoApprovalEnrolmentResponse = _postAutoAppovalEnrolmentOperator.PostAutoApprovalEnrolmentUserDetails(autoApprovalEnrolmentPayload);
            var EmailAddress = autoApprovalEnrolmentResponse.Data.Person.EmailAddress.ToString();

            GetAdssAliasHelper.accessToken = getAccessToken;
            GetAdssAliasHelper.clientId = autoApprovalEnrolmentResponse.Data.Person.ClientId.ToString();
            GetAdssAliasHelper.emailAddress = EmailAddress;

            var getAdssAliasResponse = _getAdssAliasOperator.GetAdssAliasDetails();

            var AdssAlias = getAdssAliasResponse.Data.AdssAlias.ToString();

            GetAdssDetailsHelper.accessToken = getAccessToken;
            BaseTest.ClientProductId = getClientProductId.ToString();
            BaseTest.adssAlias = AdssAlias;
            var getAdssDetailsPayload = _baseTest.GetAdssDetailsPayload();

            //Act
            var getAdssDetailsResponse = _getAdssDetailsOperator.PostGetAdssDetails(getAdssDetailsPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(getAdssDetailsResponse);
                Assert.AreEqual(true, getAdssDetailsResponse.Result.Success);
                Assert.IsNotNull(getAdssDetailsResponse.Data.Certificate);
                Assert.IsNotNull(getAdssDetailsResponse.Data.Expiry);
                Assert.IsNotNull(getAdssDetailsResponse.Data.Pkcs7);
                Assert.AreEqual("ADSS", getAdssDetailsResponse.Data.CertificateType);
                Assert.AreEqual("Successfully Recovered Key information", getAdssDetailsResponse.Data.UserFriendlyMessage);
            }
        }

        [TestMethod]
        public void VerifyThatUserCanNotGetValidAddsDetailsByUsingInValidClientProductIdAndValidAdssAlias()
        {
            //Arrange
            var officerPayload = _baseTest.GetValidAltronPayload();
            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);


            getAccessToken = officerResponse.Data.AccessToken;
            GetAdssDetailsHelper.accessToken = getAccessToken;

            BaseTest.ClientProductId = "4849059d-418a-415c-8c89-5202916c3d700";
            BaseTest.adssAlias = "7880a307-19ad-4679-9eeb-7e143999fd5b";

            var getAdssDetailsPayload = _baseTest.GetAdssDetailsPayload();

            //Act
            var getAdssDetailsResponse = _getAdssDetailsOperator.PostGetAdssDetails(getAdssDetailsPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(getAdssDetailsResponse);
                Assert.AreEqual(false, getAdssDetailsResponse.Result.Success);
                Assert.AreEqual("Please provide client product id.", getAdssDetailsResponse.Result.Error);
            }
        }

        [TestMethod]
        public void VerifyThatUserCanNotGetAdssDetailsWhenAdssAliasIsNotProvided()
        {
            //Arrange
            var officerPayload = _baseTest.GetValidAltronPayload();
            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);

            getAccessToken = officerResponse.Data.AccessToken;
            GetAdssDetailsHelper.accessToken = getAccessToken;

            BaseTest.ClientProductId = "4849059d-418a-415c-8c89-5202916c3d70";
            BaseTest.adssAlias = " ";

            var getAdssDetailsPayload = _baseTest.GetAdssDetailsPayload();

            //Act
            var getAdssDetailsResponse = _getAdssDetailsOperator.PostGetAdssDetails(getAdssDetailsPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(getAdssDetailsResponse);
                Assert.AreEqual(false, getAdssDetailsResponse.Result.Success);
                Assert.AreEqual("Please provide Adss Alias.", getAdssDetailsResponse.Result.Error);
            }
        }

        [TestMethod]
        public void VerifyThatUserCanNotGetAdssDetailsWhenClientProductIdIsNotProvided()
        {
            //Arrange
            var officerPayload = _baseTest.GetValidAltronPayload();
            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);

            getAccessToken = officerResponse.Data.AccessToken;
            GetAdssDetailsHelper.accessToken = getAccessToken;

            BaseTest.ClientProductId = "";
            BaseTest.adssAlias = "880a307-19ad-4679-9eeb-7e143999fd5b";

            var getAdssDetailsPayload = _baseTest.GetAdssDetailsPayload();

            //Act
            var getAdssDetailsResponse = _getAdssDetailsOperator.PostGetAdssDetails(getAdssDetailsPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(getAdssDetailsResponse);
                Assert.AreEqual(false, getAdssDetailsResponse.Result.Success);
                Assert.AreEqual("Please provide client product id.", getAdssDetailsResponse.Result.Error);
            }
        }
    }

    [TestClass]
    public class IssueCertificate
    {
        public PostOfficerLoginOperator _officerOperator;
        public BaseTest _baseTest;
        public GetProductsOperator _productsOperator;
        public PostSendOtpOperator _sendOtpOperator;
        public PostAutoAppovalEnrolmentOperator _postAutoApprovalEnrolmentOperator;
        public PostIssueCertificateOperator _postIssueCertificateOperator;

        public string getAccessToken;
        public Guid getClientId;
        public Guid getClientProductId;
        public Guid getOTPId;

        public IssueCertificate()
        {
            _officerOperator = new PostOfficerLoginOperator();
            _baseTest = new BaseTest();
            _productsOperator = new GetProductsOperator();
            _sendOtpOperator = new PostSendOtpOperator();
            _postAutoApprovalEnrolmentOperator = new PostAutoAppovalEnrolmentOperator();
            _postIssueCertificateOperator = new PostIssueCertificateOperator();
        }

        [TestMethod]
        public void VerifyThatACertificateCannotBeIssuedWhenPersonIdIsIncorrect()
        {
            //Arrange 
            var officerPayload = _baseTest.GetValidAltronPayload();
            var userOtpPayload = _baseTest.SendOTPValidPayload();

            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);
            getAccessToken = officerResponse.Data.AccessToken;
            getClientId = officerResponse.Data.officer.ClientId;

            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();
            var clientsProductResponse = _productsOperator.GetProductsPerClient();

            SendOTPHelper.accessToken = getAccessToken;
            var userOtpResponse = _sendOtpOperator.PostSendUserOTP(userOtpPayload);

            getClientProductId = clientsProductResponse.Data.Products[1].ClientProductId;
            getOTPId = userOtpResponse.Data.OtpId;

            STCEnrolmentAutoApproveHelper.accessToken = getAccessToken;
            BaseTest.ClientId = getClientId.ToString();
            BaseTest.ClientProductId = getClientProductId.ToString();
            BaseTest.OTPId = getOTPId.ToString();

            var autoApprovalEnrolmentPayload = _baseTest.AutoApprovalEnrolmentValidpayload();
            var autoApprovalEnrolmentResponse = _postAutoApprovalEnrolmentOperator.PostAutoApprovalEnrolmentUserDetails(autoApprovalEnrolmentPayload);

            IssueCertificateHelper.accessToken = getAccessToken;

            BaseTest.personId = "b07a161f-6329-414b-9929-aef113c36f2";
            BaseTest.csr = "".ToString();
            BaseTest.firstName = autoApprovalEnrolmentResponse.Data.Person.FirstName.ToString();
            BaseTest.lastName = autoApprovalEnrolmentResponse.Data.Person.LastName.ToString();
            BaseTest.identityNumber = autoApprovalEnrolmentResponse.Data.Person.IdentityNumber.ToString();
            BaseTest.emailAddress = autoApprovalEnrolmentResponse.Data.Person.EmailAddress.ToString();
            BaseTest.mobileNumber = autoApprovalEnrolmentResponse.Data.Person.MobileNumber.ToString();

            var issueCertificatePayload = _baseTest.GetIssueCertificatePayload();

            //Act
            var issueCertificateResponse = _postIssueCertificateOperator.PostIssueCertificateDetails(issueCertificatePayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(issueCertificateResponse);
                Assert.AreEqual(false, issueCertificateResponse.Success);
                Assert.AreEqual("Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx)", issueCertificateResponse.Message);
            }
        }

        [TestMethod]
        public void VerifyThatACertificateCannotBeIssuedWithoutPersonId()
        {
            //Arrange 
            var officerPayload = _baseTest.GetValidAltronPayload();
            var userOtpPayload = _baseTest.SendOTPValidPayload();

            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);
            getAccessToken = officerResponse.Data.AccessToken;
            getClientId = officerResponse.Data.officer.ClientId;

            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();
            var clientsProductResponse = _productsOperator.GetProductsPerClient();

            SendOTPHelper.accessToken = getAccessToken;
            var userOtpResponse = _sendOtpOperator.PostSendUserOTP(userOtpPayload);

            getClientProductId = clientsProductResponse.Data.Products[1].ClientProductId;
            getOTPId = userOtpResponse.Data.OtpId;

            STCEnrolmentAutoApproveHelper.accessToken = getAccessToken;
            BaseTest.ClientId = getClientId.ToString();
            BaseTest.ClientProductId = getClientProductId.ToString();
            BaseTest.OTPId = getOTPId.ToString();

            var autoApprovalEnrolmentPayload = _baseTest.AutoApprovalEnrolmentValidpayload();
            var autoApprovalEnrolmentResponse = _postAutoApprovalEnrolmentOperator.PostAutoApprovalEnrolmentUserDetails(autoApprovalEnrolmentPayload);

            IssueCertificateHelper.accessToken = getAccessToken;

            BaseTest.personId = "".ToString();
            BaseTest.csr = "".ToString();
            BaseTest.firstName = autoApprovalEnrolmentResponse.Data.Person.FirstName.ToString();
            BaseTest.lastName = autoApprovalEnrolmentResponse.Data.Person.LastName.ToString();
            BaseTest.identityNumber = autoApprovalEnrolmentResponse.Data.Person.IdentityNumber.ToString();
            BaseTest.emailAddress = autoApprovalEnrolmentResponse.Data.Person.EmailAddress.ToString();
            BaseTest.mobileNumber = autoApprovalEnrolmentResponse.Data.Person.MobileNumber.ToString();

            var issueCertificatePayload = _baseTest.GetIssueCertificatePayload();

            //Act
            var issueCertificateResponse = _postIssueCertificateOperator.PostIssueCertificateDetails(issueCertificatePayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(issueCertificateResponse);
                Assert.AreEqual(false, issueCertificateResponse.Success);
                Assert.AreEqual("Unrecognized Guid format", issueCertificateResponse.Message);
            }
        }

        [TestMethod]
        public void VerifyThatACertificateCannotBeIssuedifPersonIdIsNull()
        {
            //Arrange 
            var officerPayload = _baseTest.GetValidAltronPayload();
            var userOtpPayload = _baseTest.SendOTPValidPayload();

            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);
            getAccessToken = officerResponse.Data.AccessToken;
            getClientId = officerResponse.Data.officer.ClientId;

            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();
            var clientsProductResponse = _productsOperator.GetProductsPerClient();

            SendOTPHelper.accessToken = getAccessToken;
            var userOtpResponse = _sendOtpOperator.PostSendUserOTP(userOtpPayload);

            getClientProductId = clientsProductResponse.Data.Products[1].ClientProductId;
            getOTPId = userOtpResponse.Data.OtpId;

            STCEnrolmentAutoApproveHelper.accessToken = getAccessToken;
            BaseTest.ClientId = getClientId.ToString();
            BaseTest.ClientProductId = getClientProductId.ToString();
            BaseTest.OTPId = getOTPId.ToString();

            var autoApprovalEnrolmentPayload = _baseTest.AutoApprovalEnrolmentValidpayload();
            var autoApprovalEnrolmentResponse = _postAutoApprovalEnrolmentOperator.PostAutoApprovalEnrolmentUserDetails(autoApprovalEnrolmentPayload);

            IssueCertificateHelper.accessToken = getAccessToken;

            BaseTest.personId = null;
            BaseTest.csr = "".ToString();
            BaseTest.firstName = autoApprovalEnrolmentResponse.Data.Person.FirstName.ToString();
            BaseTest.lastName = autoApprovalEnrolmentResponse.Data.Person.LastName.ToString();
            BaseTest.identityNumber = autoApprovalEnrolmentResponse.Data.Person.IdentityNumber.ToString();
            BaseTest.emailAddress = autoApprovalEnrolmentResponse.Data.Person.EmailAddress.ToString();
            BaseTest.mobileNumber = autoApprovalEnrolmentResponse.Data.Person.MobileNumber.ToString();

            var issueCertificatePayload = _baseTest.GetIssueCertificatePayload();

            //Act
            var issueCertificateResponse = _postIssueCertificateOperator.PostIssueCertificateDetails(issueCertificatePayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(issueCertificateResponse);
                Assert.AreEqual(false, issueCertificateResponse.Success);
                Assert.AreEqual("Value cannot be null", issueCertificateResponse.Message);
            }
        }
    }

    [TestClass]
    public class GetAdssCertificateDetails
    {
        public PostOfficerLoginOperator _officerOperator;
        public BaseTest _baseTest;
        public GetProductsOperator _productsOperator;
        public GetAdssCertificateDetailsOperator _getAdssCertificateDetailsOperator;
        public PostSendOtpOperator _sendOtpOperator;
        public PostAutoAppovalEnrolmentOperator _postAutoAppovalEnrolmentOperator;
        public Guid getPersonId;
        public Guid getClientId;
        public Guid getClientProductId;
        public Guid getOTPId;

        public string getAccessToken;


        public GetAdssCertificateDetails()
        {
            _productsOperator = new GetProductsOperator();
            _sendOtpOperator = new PostSendOtpOperator();
            _postAutoAppovalEnrolmentOperator = new PostAutoAppovalEnrolmentOperator();
            _officerOperator = new PostOfficerLoginOperator();
            _baseTest = new BaseTest();
            _getAdssCertificateDetailsOperator = new GetAdssCertificateDetailsOperator();
        }

        [TestMethod]
        public void VerifyThatAdssCertificateDetailsCanBeRetrivedSuccessfully()
        {
            //Arrange 
            var officerPayload = _baseTest.GetValidAltronPayload();
            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);
            getAccessToken = officerResponse.Data.AccessToken;
            getClientId = officerResponse.Data.officer.ClientId;

            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();
            var clientProductResponse = _productsOperator.GetProductsPerClient();

            SendOTPHelper.accessToken = getAccessToken;
            var userOtpPayload = _baseTest.SendOTPValidPayload();
            var userOTPResponse = _sendOtpOperator.PostSendUserOTP(userOtpPayload);

            getClientProductId = clientProductResponse.Data.Products[0].ClientProductId;
            getOTPId = userOTPResponse.Data.OtpId;

            STCEnrolmentAutoApproveHelper.accessToken = getAccessToken;
            BaseTest.ClientId = getClientId.ToString();
            BaseTest.ClientProductId = getClientProductId.ToString();
            BaseTest.OTPId = getOTPId.ToString();

            var autoApprovalEnrolmentPayload = _baseTest.AutoApprovalEnrolmentValidpayload();
            var autoApprovalEnrolmentResponse = _postAutoAppovalEnrolmentOperator.PostAutoApprovalEnrolmentUserDetails(autoApprovalEnrolmentPayload);

            var getPersonId = autoApprovalEnrolmentResponse.Data.Person.PersonId.ToString();
       
            GetAdssCertificateDetailsHelper.accessToken = getAccessToken;
            GetAdssCertificateDetailsHelper.personId = getPersonId;
            GetAdssCertificateDetailsHelper.clientProductId = getClientProductId.ToString();

            //Act
            var adssCertificateDetailsResponse = _getAdssCertificateDetailsOperator.GetAdssCertificateDetails();

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(adssCertificateDetailsResponse);
                Assert.AreEqual(true, adssCertificateDetailsResponse.Result.Success);
                Assert.IsNotNull(adssCertificateDetailsResponse.Data.Expiry);
                Assert.AreEqual("ADSS", adssCertificateDetailsResponse.Data.CertificateType);
                Assert.AreEqual("Successfully Recovered Key information", adssCertificateDetailsResponse.Data.UserFriendlyMessage);
                Assert.IsNotNull(adssCertificateDetailsResponse.Data.Certificate);
                Assert.IsNotNull(adssCertificateDetailsResponse.Data.Pkcs7);
            }
        }

        [TestMethod]
        public void VerifyThatRASCertificateDetailsCanBeRetrievedWhenClientProductIdIsForGoSign()
        {
            //Arrange 
            var officerPayload = _baseTest.GetValidAltronPayload();
            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);
            getAccessToken = officerResponse.Data.AccessToken;
            getClientId = officerResponse.Data.officer.ClientId;

            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();
            var clientProductResponse = _productsOperator.GetProductsPerClient();

            SendOTPHelper.accessToken = getAccessToken;
            var userOtpPayload = _baseTest.SendOTPValidPayload();
            var userOTPResponse = _sendOtpOperator.PostSendUserOTP(userOtpPayload);

            getClientProductId = clientProductResponse.Data.Products[4].ClientProductId;
            getOTPId = userOTPResponse.Data.OtpId;

            STCEnrolmentAutoApproveHelper.accessToken = getAccessToken;
            BaseTest.ClientId = getClientId.ToString();
            BaseTest.ClientProductId = getClientProductId.ToString();
            BaseTest.OTPId = getOTPId.ToString();

            var autoApprovalEnrolmentPayload = _baseTest.AutoApprovalEnrolmentValidpayload();
            var autoApprovalEnrolmentResponse = _postAutoAppovalEnrolmentOperator.PostAutoApprovalEnrolmentUserDetails(autoApprovalEnrolmentPayload);

            var getPersonId = autoApprovalEnrolmentResponse.Data.Person.PersonId.ToString();

            GetAdssCertificateDetailsHelper.accessToken = getAccessToken;
            GetAdssCertificateDetailsHelper.personId = getPersonId.ToString();
             GetAdssCertificateDetailsHelper.clientProductId = "c24ca545-7a65-4b37-983a-73c956f05d4b".ToString();

            //Act
            var rasCertificateDetailsResponse = _getAdssCertificateDetailsOperator.GetAdssCertificateDetails();

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(rasCertificateDetailsResponse);
                Assert.AreEqual(true, rasCertificateDetailsResponse.Result.Success);
                Assert.IsNotNull(rasCertificateDetailsResponse.Data.Expiry);
                Assert.AreEqual("RAS", rasCertificateDetailsResponse.Data.CertificateType);
                Assert.AreEqual("Successfully Recovered Key information", rasCertificateDetailsResponse.Data.UserFriendlyMessage);
                Assert.IsNotNull(rasCertificateDetailsResponse.Data.Certificate);
                Assert.IsNotNull(rasCertificateDetailsResponse.Data.Pkcs7);
            }
        }

        [TestMethod]
        public void VerifyThatAdssCertificateDetailsCannotBeRetrievedWithClientProductIdThatDoesNotExist()
        {
            //Arrange 
            var officerPayload = _baseTest.GetValidAltronPayload();
            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);
            getAccessToken = officerResponse.Data.AccessToken;
            getClientId = officerResponse.Data.officer.ClientId;

            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();
            var clientProductResponse = _productsOperator.GetProductsPerClient();

            SendOTPHelper.accessToken = getAccessToken;
            var userOtpPayload = _baseTest.SendOTPValidPayload();
            var userOTPResponse = _sendOtpOperator.PostSendUserOTP(userOtpPayload);

            getClientProductId = clientProductResponse.Data.Products[1].ClientProductId;
            getOTPId = userOTPResponse.Data.OtpId;

            STCEnrolmentAutoApproveHelper.accessToken = getAccessToken;
            BaseTest.ClientId = getClientId.ToString();
            BaseTest.ClientProductId = getClientProductId.ToString();
            BaseTest.OTPId = getOTPId.ToString();

            var autoApprovalEnrolmentPayload = _baseTest.AutoApprovalEnrolmentValidpayload();
            var autoApprovalEnrolmentResponse = _postAutoAppovalEnrolmentOperator.PostAutoApprovalEnrolmentUserDetails(autoApprovalEnrolmentPayload);

            var getPersonId = autoApprovalEnrolmentResponse.Data.Person.PersonId.ToString();

            GetAdssCertificateDetailsHelper.accessToken = getAccessToken;
            GetAdssCertificateDetailsHelper.personId = getPersonId;
            GetAdssCertificateDetailsHelper.clientProductId = "4849059D-418A-415C-8C89-5202916C3D78".ToString();

            //Act
            var adssCertificateDetailsResponse = _getAdssCertificateDetailsOperator.GetAdssCertificateDetails();

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(adssCertificateDetailsResponse);
                Assert.AreEqual(false, adssCertificateDetailsResponse.Result.Success);
                Assert.AreEqual("Client Product <4849059d-418a-415c-8c89-5202916c3d78> Settings Not Found", adssCertificateDetailsResponse.Result.Error);
            }
        }

        [TestMethod]
        public void VerifyThatAdssCertificateDetailsCannotBeRetrievedWhenClientProductIdIsForSigningHub()
        {
            //Arrange 
            var officerPayload = _baseTest.GetValidAltronPayload();
            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);
            getAccessToken = officerResponse.Data.AccessToken;
            getClientId = officerResponse.Data.officer.ClientId;

            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();
            var clientProductResponse = _productsOperator.GetProductsPerClient();

            SendOTPHelper.accessToken = getAccessToken;
            var userOtpPayload = _baseTest.SendOTPValidPayload();
            var userOTPResponse = _sendOtpOperator.PostSendUserOTP(userOtpPayload);

            getClientProductId = clientProductResponse.Data.Products[2].ClientProductId;
            getOTPId = userOTPResponse.Data.OtpId;

            STCEnrolmentAutoApproveHelper.accessToken = getAccessToken;
            BaseTest.ClientId = getClientId.ToString();
            BaseTest.ClientProductId = getClientProductId.ToString();
            BaseTest.OTPId = getOTPId.ToString();

            var autoApprovalEnrolmentPayload = _baseTest.AutoApprovalEnrolmentValidpayload();
            var autoApprovalEnrolmentResponse = _postAutoAppovalEnrolmentOperator.PostAutoApprovalEnrolmentUserDetails(autoApprovalEnrolmentPayload);

            var getPersonId = autoApprovalEnrolmentResponse.Data.Person.PersonId.ToString();

            GetAdssCertificateDetailsHelper.accessToken = getAccessToken;
            GetAdssCertificateDetailsHelper.personId = getPersonId;
            GetAdssCertificateDetailsHelper.clientProductId = "4B02D56F-ABD6-4F49-A8AE-F72E3B3D33C2".ToString();

            //Act
            var adssCertificateDetailsResponse = _getAdssCertificateDetailsOperator.GetAdssCertificateDetails();

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(adssCertificateDetailsResponse);
                Assert.AreEqual(false, adssCertificateDetailsResponse.Result.Success);
                Assert.AreEqual("ClientProductId is not for ADSS or RAS", adssCertificateDetailsResponse.Result.Error);
            }
        }

        [TestMethod]
        public void VerifyThatAdssCertificateDetailsCannotBeRetrievedWhenPersonIdDoesNotHaveADSS()
        {
            //Arrange 
            var officerPayload = _baseTest.GetValidAltronPayload();
            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);
            getAccessToken = officerResponse.Data.AccessToken;
            getClientId = officerResponse.Data.officer.ClientId;

            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();
            var clientProductResponse = _productsOperator.GetProductsPerClient();

            SendOTPHelper.accessToken = getAccessToken;
            var userOtpPayload = _baseTest.SendOTPValidPayload();
            var userOTPResponse = _sendOtpOperator.PostSendUserOTP(userOtpPayload);

            getClientProductId = clientProductResponse.Data.Products[4].ClientProductId;
            getOTPId = userOTPResponse.Data.OtpId;

            STCEnrolmentAutoApproveHelper.accessToken = getAccessToken;
            BaseTest.ClientId = getClientId.ToString();
            BaseTest.ClientProductId = getClientProductId.ToString();
            BaseTest.OTPId = getOTPId.ToString();

            var autoApprovalEnrolmentPayload = _baseTest.AutoApprovalEnrolmentValidpayload();
            var autoApprovalEnrolmentResponse = _postAutoAppovalEnrolmentOperator.PostAutoApprovalEnrolmentUserDetails(autoApprovalEnrolmentPayload);

            var getPersonId = autoApprovalEnrolmentResponse.Data.Person.PersonId.ToString();

            GetAdssCertificateDetailsHelper.accessToken = getAccessToken;
            GetAdssCertificateDetailsHelper.personId = "6DD0710D-AE94-4995-B8B1-3131596F503F";
            GetAdssCertificateDetailsHelper.clientProductId = getClientProductId.ToString();

            //Act
            var adssCertificateDetailsResponse = _getAdssCertificateDetailsOperator.GetAdssCertificateDetails();

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(adssCertificateDetailsResponse);
                Assert.AreEqual(false, adssCertificateDetailsResponse.Result.Success);
                Assert.AreEqual("Person Not Found", adssCertificateDetailsResponse.Result.Error);
            }
        }

        [TestMethod]
        public void VerifyThatAdssCertificateDetailsCannotBeRetrievedWhenPersonIdIsInvalid()
        {
            //Arrange 
            var officerPayload = _baseTest.GetValidAltronPayload();
            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);
            getAccessToken = officerResponse.Data.AccessToken;
            getClientId = officerResponse.Data.officer.ClientId;

            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();
            var clientProductResponse = _productsOperator.GetProductsPerClient();

            SendOTPHelper.accessToken = getAccessToken;
            var userOtpPayload = _baseTest.SendOTPValidPayload();
            var userOTPResponse = _sendOtpOperator.PostSendUserOTP(userOtpPayload);

            getClientProductId = clientProductResponse.Data.Products[1].ClientProductId;
            getOTPId = userOTPResponse.Data.OtpId;

            STCEnrolmentAutoApproveHelper.accessToken = getAccessToken;
            BaseTest.ClientId = getClientId.ToString();
            BaseTest.ClientProductId = getClientProductId.ToString();
            BaseTest.OTPId = getOTPId.ToString();

            var autoApprovalEnrolmentPayload = _baseTest.AutoApprovalEnrolmentValidpayload();
            var autoApprovalEnrolmentResponse = _postAutoAppovalEnrolmentOperator.PostAutoApprovalEnrolmentUserDetails(autoApprovalEnrolmentPayload);

            var getPersonId = autoApprovalEnrolmentResponse.Data.Person.PersonId.ToString();

            GetAdssCertificateDetailsHelper.accessToken = getAccessToken;
            GetAdssCertificateDetailsHelper.personId = "B55CF6A2-07DE-4213-AB50-72B67B1B8B7F";
            GetAdssCertificateDetailsHelper.clientProductId = getClientProductId.ToString();

            //Act
            var adssCertificateDetailsResponse = _getAdssCertificateDetailsOperator.GetAdssCertificateDetails();

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(adssCertificateDetailsResponse);
                Assert.AreEqual(false, adssCertificateDetailsResponse.Result.Success);
                Assert.AreEqual("Person Not Found", adssCertificateDetailsResponse.Result.Error);
            }
        }

        [TestMethod]
        public void VerifyThatAdssCertificateDetailsCannotBeRetrievedWhenClientProductIdDoesNotHaveADSSSettings()
        {
            //Arrange 
            var officerPayload = _baseTest.GetValidAltronPayload();
            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);
            getAccessToken = officerResponse.Data.AccessToken;
            getClientId = officerResponse.Data.officer.ClientId;

            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();
            var clientProductResponse = _productsOperator.GetProductsPerClient();

            SendOTPHelper.accessToken = getAccessToken;
            var userOtpPayload = _baseTest.SendOTPValidPayload();
            var userOTPResponse = _sendOtpOperator.PostSendUserOTP(userOtpPayload);

            getClientProductId = clientProductResponse.Data.Products[1].ClientProductId;
            getOTPId = userOTPResponse.Data.OtpId;

            STCEnrolmentAutoApproveHelper.accessToken = getAccessToken;
            BaseTest.ClientId = getClientId.ToString();
            BaseTest.ClientProductId = getClientProductId.ToString();
            BaseTest.OTPId = getOTPId.ToString();

            var autoApprovalEnrolmentPayload = _baseTest.AutoApprovalEnrolmentValidpayload();
            var autoApprovalEnrolmentResponse = _postAutoAppovalEnrolmentOperator.PostAutoApprovalEnrolmentUserDetails(autoApprovalEnrolmentPayload);

            var getPersonId = autoApprovalEnrolmentResponse.Data.Person.PersonId.ToString();

            GetAdssCertificateDetailsHelper.accessToken = getAccessToken;
            GetAdssCertificateDetailsHelper.personId = getPersonId.ToString();
            GetAdssCertificateDetailsHelper.clientProductId = "1229f02e-9194-4d1d-89dc-c9102d327495".ToString();

            //Act
            var adssCertificateDetailsResponse = _getAdssCertificateDetailsOperator.GetAdssCertificateDetails();

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(adssCertificateDetailsResponse);
                Assert.AreEqual(false, adssCertificateDetailsResponse.Result.Success);
                Assert.AreEqual("Client Product <1229f02e-9194-4d1d-89dc-c9102d327495> Settings Not Found", adssCertificateDetailsResponse.Result.Error);
            }
        }
    }

    [TestClass]
    public class AutoApproveSelfPDO
    {
        public PostOfficerLoginOperator _officerOperator;
        public GetProductsOperator _productsOperator;
        public PostSendOtpOperator _sendOtpOperator;
        public PostAutoApproveSelfPDOOperator _postAutoApproveSelfPDOOperator;
        public BaseTest _baseTest;

        public string getAccessToken;
        public Guid getClientId;
        public Guid getOTPId;
        public Guid getClientProductId;


        public AutoApproveSelfPDO()
        {
            _productsOperator = new GetProductsOperator();
            _officerOperator = new PostOfficerLoginOperator();
            _sendOtpOperator = new PostSendOtpOperator();
            _postAutoApproveSelfPDOOperator = new PostAutoApproveSelfPDOOperator();
            _baseTest = new BaseTest();
        }

        [TestMethod]
        public void VerifyThataUserCanAutoEnrolForSigningHub07UsingTheAutoApproveSelfPDO()
        {
            //Arrange 
            var officerPayload = _baseTest.GetValidAltronPayload();
            var userOtpPayload = _baseTest.SendOTPValidPayload();

            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);
            getAccessToken = officerResponse.Data.AccessToken;
            getClientId = officerResponse.Data.officer.ClientId;

            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();

            var clientsProductResponse = _productsOperator.GetProductsPerClient();

            getClientProductId = clientsProductResponse.Data.Products[3].ClientProductId;

            SendOTPHelper.accessToken = getAccessToken;
            var userOtpResponse = _sendOtpOperator.PostSendUserOTP(userOtpPayload);

            getOTPId = userOtpResponse.Data.OtpId;

            AutoApproveSelfPDOHelper.accessToken = getAccessToken;
            BaseTest.ClientId = getClientId.ToString();
            BaseTest.ClientProductId = getClientProductId.ToString();
            BaseTest.OTPId = getOTPId.ToString();
            BaseTest.businessUnit = "Auditing Department";

            var autoApprovSelfPdoEnrolmentPayload = _baseTest.AutoApproveSelfPDOEnrolmentValidpayload();

            // Act
            var autoApproveSelfPdoEnrolmentResponse = _postAutoApproveSelfPDOOperator.PostAutoApproveSelfPdofEnrolmentUserDetails(autoApprovSelfPdoEnrolmentPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(autoApproveSelfPdoEnrolmentResponse);
                Assert.AreEqual(true, autoApproveSelfPdoEnrolmentResponse.Result.Success);
                Assert.IsNotNull(autoApproveSelfPdoEnrolmentResponse.Data.Person.ClientId);
            }
        }

        [TestMethod]
        public void VerifyThataUserCanAutoEnrolForSigningHub10UsingTheAutoApproveSelfPDO()
        {
            //Arrange 
            var officerPayload = _baseTest.GetValidAltronPayload();
            var userOtpPayload = _baseTest.SendOTPValidPayload();

            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);
            getAccessToken = officerResponse.Data.AccessToken;
            getClientId = officerResponse.Data.officer.ClientId;

            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();

            var clientsProductResponse = _productsOperator.GetProductsPerClient();

            getClientProductId = clientsProductResponse.Data.Products[2].ClientProductId;

            SendOTPHelper.accessToken = getAccessToken;
            var userOtpResponse = _sendOtpOperator.PostSendUserOTP(userOtpPayload);

            getOTPId = userOtpResponse.Data.OtpId;

            AutoApproveSelfPDOHelper.accessToken = getAccessToken;
            BaseTest.ClientId = getClientId.ToString();
            BaseTest.ClientProductId = getClientProductId.ToString();
            BaseTest.OTPId = getOTPId.ToString();
            BaseTest.businessUnit = "Auditing Department";

            var autoApprovSelfPdoEnrolmentPayload = _baseTest.AutoApproveSelfPDOEnrolmentValidpayload();

            // Act
            var autoApproveSelfPdoEnrolmentResponse = _postAutoApproveSelfPDOOperator.PostAutoApproveSelfPdofEnrolmentUserDetails(autoApprovSelfPdoEnrolmentPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(autoApproveSelfPdoEnrolmentResponse);
                Assert.AreEqual(true, autoApproveSelfPdoEnrolmentResponse.Result.Success);
                Assert.IsNotNull(autoApproveSelfPdoEnrolmentResponse.Data.Person.ClientId);
            }
        }

        [TestMethod]
        public void VerifyThataUserCanAutoEnrolForADSSUsingTheAutoApproveSelfPDO()
        {
            //Arrange 
            var officerPayload = _baseTest.GetValidAltronPayload();
            var userOtpPayload = _baseTest.SendOTPValidPayload();

            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);
            getAccessToken = officerResponse.Data.AccessToken;
            getClientId = officerResponse.Data.officer.ClientId;

            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();

            var clientsProductResponse = _productsOperator.GetProductsPerClient();

            getClientProductId = clientsProductResponse.Data.Products[0].ClientProductId;

            SendOTPHelper.accessToken = getAccessToken;
            var userOtpResponse = _sendOtpOperator.PostSendUserOTP(userOtpPayload);

            getOTPId = userOtpResponse.Data.OtpId;

            AutoApproveSelfPDOHelper.accessToken = getAccessToken;
            BaseTest.ClientId = getClientId.ToString();
            BaseTest.ClientProductId = getClientProductId.ToString();
            BaseTest.OTPId = getOTPId.ToString();
            BaseTest.businessUnit = "Auditing Department";

            var autoApprovSelfPdoEnrolmentPayload = _baseTest.AutoApproveSelfPDOEnrolmentValidpayload();

            // Act
            var autoApproveSelfPdoEnrolmentResponse = _postAutoApproveSelfPDOOperator.PostAutoApproveSelfPdofEnrolmentUserDetails(autoApprovSelfPdoEnrolmentPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(autoApproveSelfPdoEnrolmentResponse);
                Assert.AreEqual(true, autoApproveSelfPdoEnrolmentResponse.Result.Success);
                Assert.IsNotNull(autoApproveSelfPdoEnrolmentResponse.Data.Person.ClientId);
            }
        }

        [TestMethod]
        public void VerifyThataUserCanAutoEnrolForRASUsingTheAutoApproveSelfPDO()
        {
            //Arrange 
            var officerPayload = _baseTest.GetValidAltronPayload();
            var userOtpPayload = _baseTest.SendOTPValidPayload();

            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);
            getAccessToken = officerResponse.Data.AccessToken;
            getClientId = officerResponse.Data.officer.ClientId;

            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();

            var clientsProductResponse = _productsOperator.GetProductsPerClient();

            getClientProductId = clientsProductResponse.Data.Products[4].ClientProductId;

            SendOTPHelper.accessToken = getAccessToken;
            var userOtpResponse = _sendOtpOperator.PostSendUserOTP(userOtpPayload);

            getOTPId = userOtpResponse.Data.OtpId;

            AutoApproveSelfPDOHelper.accessToken = getAccessToken;
            BaseTest.ClientId = getClientId.ToString();
            BaseTest.ClientProductId = getClientProductId.ToString();
            BaseTest.OTPId = getOTPId.ToString();
            BaseTest.businessUnit = "Auditing Department";

            var autoApprovSelfPdoEnrolmentPayload = _baseTest.AutoApproveSelfPDOEnrolmentValidpayload();

            // Act
            var autoApproveSelfPdoEnrolmentResponse = _postAutoApproveSelfPDOOperator.PostAutoApproveSelfPdofEnrolmentUserDetails(autoApprovSelfPdoEnrolmentPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(autoApproveSelfPdoEnrolmentResponse);
                Assert.AreEqual(true, autoApproveSelfPdoEnrolmentResponse.Result.Success);
                Assert.IsNotNull(autoApproveSelfPdoEnrolmentResponse.Data.Person.ClientId);
            }
        }

        [TestMethod]
        public void VerifyThatAuserCannotAutoEnrolSelfPdoTUsingExistingEmailAddress()
        {
            //Arrange 
            var officerPayload = _baseTest.GetValidAltronPayload();
            var userOtpPayload = _baseTest.SendOTPValidPayload();

            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);
            getAccessToken = officerResponse.Data.AccessToken;
            getClientId = officerResponse.Data.officer.ClientId;

            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();

            SendOTPHelper.accessToken = getAccessToken;
            var userOtpResponse = _sendOtpOperator.PostSendUserOTP(userOtpPayload);

            getOTPId = userOtpResponse.Data.OtpId;

            AutoApproveSelfPDOHelper.accessToken = getAccessToken;
            BaseTest.ClientId = getClientId.ToString();
            BaseTest.emailAddress = "AlreadyExist@Altron.com".ToString();
            BaseTest.OTPId = getOTPId.ToString();
            BaseTest.businessUnit = "Auditing Department";

            var autoApproveSelfPdoEnrolmentPayload = _baseTest.AutoApproveSelfPDOEnrolmentEmailInValidpayload();

            // Act
            var autoApproveSelfPdoEnrolmentResponse = _postAutoApproveSelfPDOOperator.PostAutoApproveSelfPdofEnrolmentUserDetails(autoApproveSelfPdoEnrolmentPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(autoApproveSelfPdoEnrolmentResponse);
                Assert.AreEqual(false, autoApproveSelfPdoEnrolmentResponse.Result.Success);
                Assert.AreEqual("Applicant with the same email address already exists.", autoApproveSelfPdoEnrolmentResponse.Result.Error);
            }
        }

        [TestMethod]
        public void VerifyThatAuserCanotAutoEnrollWithoutaClientId()
        {
            //Arrange 
            var officerPayload = _baseTest.GetValidAltronPayload();
            var userOtpPayload = _baseTest.SendOTPValidPayload();

            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);
            getAccessToken = officerResponse.Data.AccessToken;
            getClientId = officerResponse.Data.officer.ClientId;

            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();

            SendOTPHelper.accessToken = getAccessToken;
            var userOtpResponse = _sendOtpOperator.PostSendUserOTP(userOtpPayload);

            getOTPId = userOtpResponse.Data.OtpId;

            AutoApproveSelfPDOHelper.accessToken = getAccessToken;
            BaseTest.ClientId = "".ToString();
            BaseTest.OTPId = getOTPId.ToString();
            BaseTest.businessUnit = "Auditing Department";

            var autoApprovSelfPdoEnrolmentPayload = _baseTest.AutoApproveSelfPDOEnrolmentValidpayload();

            // Act
            var autoApproveSelfPdoEnrolmentResponse = _postAutoApproveSelfPDOOperator.PostAutoApproveSelfPdofEnrolmentUserDetails(autoApprovSelfPdoEnrolmentPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(autoApproveSelfPdoEnrolmentResponse);
                Assert.AreEqual(false, autoApproveSelfPdoEnrolmentResponse.Result.Success);
                Assert.AreEqual("Please provide ClientId.", autoApproveSelfPdoEnrolmentResponse.Result.Error);
            }
        }

        [TestMethod]
        public void VerifyThatAuserCannotAutoEnrollWithoutAClientProductId()
        {
            //Arrange 
            var officerPayload = _baseTest.GetValidAltronPayload();
            var userOtpPayload = _baseTest.SendOTPValidPayload();

            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);
            getAccessToken = officerResponse.Data.AccessToken;
            getClientId = officerResponse.Data.officer.ClientId;

            ProductHelper.accessToken = getAccessToken;
            ProductHelper.clientId = getClientId.ToString();

            SendOTPHelper.accessToken = getAccessToken;
            var userOtpResponse = _sendOtpOperator.PostSendUserOTP(userOtpPayload);

            getOTPId = userOtpResponse.Data.OtpId;

            AutoApproveSelfPDOHelper.accessToken = getAccessToken;
            BaseTest.ClientId = getClientId.ToString();
            BaseTest.OTPId = getOTPId.ToString();
            BaseTest.businessUnit = "Auditing Department";
            BaseTest.ClientProductId = "";

            var autoApprovSelfPdoEnrolmentPayload = _baseTest.AutoApproveSelfPdoEnrolmentInValidpayload();

            // Act
            var autoApproveSelfPdoEnrolmentResponse = _postAutoApproveSelfPDOOperator.PostAutoApproveSelfPdofEnrolmentUserDetails(autoApprovSelfPdoEnrolmentPayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(autoApproveSelfPdoEnrolmentResponse);
                Assert.AreEqual(false, autoApproveSelfPdoEnrolmentResponse.Result.Success);
                Assert.AreEqual("ClientProductIds list must not be empty.", autoApproveSelfPdoEnrolmentResponse.Result.Error);
            }
        }
    }

    [TestClass]
    public class RevokeAdssCertificate
    {
        public PostOfficerLoginOperator _officerOperator;
        public PostRevokeAdssCertificateOperator _postRevokeAdssCertificateOperator;
        public BaseTest _baseTest;
        public string getAccessToken;
        public Guid getClientId;
        
        public RevokeAdssCertificate()
        {
            _officerOperator = new PostOfficerLoginOperator();
            _postRevokeAdssCertificateOperator = new PostRevokeAdssCertificateOperator();
            _baseTest = new BaseTest();
        }

        [TestMethod]
        public void VerifyThatAnApplicantCanRevokeAdssCertificateApplicationSuccessfully()
        {
            //Arrange 
            var officerPayload = _baseTest.GetValidAltronPayload();
            var userOtpPayload = _baseTest.SendOTPValidPayload();

            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);
            getAccessToken = officerResponse.Data.AccessToken;
            getClientId = officerResponse.Data.officer.ClientId;

            RevokeAdssCertificateHelper.accessToken = getAccessToken;
            BaseTest.personId = "0be4591d-6a09-4f9f-a553-28c53f3fcec0";
            BaseTest.ClientProductId = "bf9b58db-c127-47ab-a2d9-1b885f96652a";
            BaseTest.AdssPassword = "Testing123";
            BaseTest.adssAlias = "Tes96146962-9a31-4ccf-90e3-78c2ebd6ff91ting";

            var revokeAdssCertificatePayload = _baseTest.RevokeAdssCertificatePayload();

            //Act
            var revokeAdssCertificateResponse = _postRevokeAdssCertificateOperator.PostRevokeAdssCertificate(revokeAdssCertificatePayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(revokeAdssCertificateResponse);
                Assert.AreEqual("Certificate Revoked Successfully!", revokeAdssCertificateResponse.Data.UserFriendlyMessage);
                Assert.AreEqual(true, revokeAdssCertificateResponse.Result.Success);
            }
        }

        [TestMethod]
        public void VerifyThatUserIsUnableToRevokeAdssSuccessfullyUsingNullPersonId()
        {
            //Arrage
            var officerPayload = _baseTest.GetValidAltronPayload();

            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);

            getAccessToken = officerResponse.Data.AccessToken;

            RevokeAdssCertificateHelper.accessToken = getAccessToken;
            BaseTest.personId = "";
            BaseTest.ClientProductId = "bf9b58db-c127-47ab-a2d9-1b885f96652a";
            BaseTest.AdssPassword = "Testing";
            BaseTest.adssAlias = "Tes96146962-9a31-4ccf-90e3-78c2ebd6ff91ting";

            var revokeAdssCertificatePayload = _baseTest.RevokeAdssCertificatePayload();

            //Act
            var revokeAdssCertificateResponse = _postRevokeAdssCertificateOperator.PostRevokeAdssCertificate(revokeAdssCertificatePayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(revokeAdssCertificateResponse);
                Assert.AreEqual(false, revokeAdssCertificateResponse.Result.Success);
                Assert.AreEqual("Please provide a PersonId.", revokeAdssCertificateResponse.Result.Error);
            }
        }

        [TestMethod]
        public void VerifyThatUserIsUnableToRevokeAdssSuccessfullyUsingNullClientProductId()
        {
            //Arrage
            var officerPayload = _baseTest.GetValidAltronPayload();

            var officerResponse = _officerOperator.UpdateOfficerLogin(officerPayload);

            getAccessToken = officerResponse.Data.AccessToken;

            RevokeAdssCertificateHelper.accessToken = getAccessToken;
            BaseTest.personId = "0be4591d-6a09-4f9f-a553-28c53f3fcec0";
            BaseTest.ClientProductId = "";
            BaseTest.AdssPassword = "Testing";
            BaseTest.adssAlias = "Tes96146962-9a31-4ccf-90e3-78c2ebd6ff91ting";

            var revokeAdssCertificatePayload = _baseTest.RevokeAdssCertificatePayload();

            //Act
            var revokeAdssCertificateResponse = _postRevokeAdssCertificateOperator.PostRevokeAdssCertificate(revokeAdssCertificatePayload);

            //Assert
            using (new AssertionScope())
            {
                Assert.IsNotNull(revokeAdssCertificateResponse);
                Assert.AreEqual(false, revokeAdssCertificateResponse.Result.Success);
                Assert.AreEqual("Please provide a ClientProductId.", revokeAdssCertificateResponse.Result.Error);
            }
        }
        
    }
}







