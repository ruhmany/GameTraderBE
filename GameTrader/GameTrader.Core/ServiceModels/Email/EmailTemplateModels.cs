namespace GameTrader.Core.ServiceModels.Email
{
    public class EmailTemplateModels
    {

        public static string GetEmailConfirmationTemplate(string confirmationUrl, string userName, string companyName, string email, string otpCode)
        {
            var greeting = !string.IsNullOrEmpty(userName) ? $"Hi {userName}," : "Hello,";

            return $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Your OTP Code</title>
    <style>
        body {{
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            line-height: 1.6;
            color: #333;
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
            background-color: #f8f9fa;
        }}
        .email-container {{
            background-color: #ffffff;
            border-radius: 10px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
            overflow: hidden;
        }}
        .header {{
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            padding: 40px 30px;
            text-align: center;
        }}
        .header h1 {{
            margin: 0;
            font-size: 28px;
            font-weight: 300;
        }}
        .content {{
            padding: 40px 30px;
        }}
        .greeting {{
            font-size: 18px;
            margin-bottom: 20px;
            color: #2c3e50;
        }}
        .message {{
            font-size: 16px;
            margin-bottom: 30px;
            line-height: 1.8;
        }}
        .otp-code {{
            font-size: 32px;
            font-weight: bold;
            letter-spacing: 4px;
            text-align: center;
            background-color: #f1f3f5;
            padding: 20px;
            border-radius: 10px;
            margin: 20px 0;
            color: #2d3748;
        }}
        .security-notice {{
            margin-top: 30px;
            padding: 20px;
            background-color: #fff3cd;
            border: 1px solid #ffeaa7;
            border-radius: 8px;
            color: #856404;
        }}
        .security-notice strong {{
            color: #b45309;
        }}
        .footer {{
            background-color: #f8f9fa;
            padding: 30px;
            text-align: center;
            border-top: 1px solid #e9ecef;
        }}
        .footer p {{
            margin: 5px 0;
            font-size: 14px;
            color: #6c757d;
        }}
        @media (max-width: 600px) {{
            .content {{
                padding: 30px 20px;
            }}
            .header {{
                padding: 30px 20px;
            }}
        }}
    </style>
</head>
<body>
    <div class='email-container'>
        <div class='header'>
            <h1>🔐 Your One-Time Password (OTP)</h1>
        </div>
        
        <div class='content'>
            <div class='greeting'>{greeting}</div>
            
            <div class='message'>
                <p>Use the following OTP to complete your action with <strong>{companyName}</strong>. This code is valid for a limited time (15 Minutes).</p>
            </div>
            
            <div class='otp-code'>{otpCode}</div>
            
            <div class='security-notice'>
                <p><strong>⚠️ Security Notice:</strong> Do not share this code with anyone. If you didn’t request this OTP, please ignore this email.</p>
            </div>
        </div>
        
        <div class='footer'>
            <p><strong>{companyName}</strong></p>
            <p style='margin-top: 20px; font-size: 12px;'>
                This email was sent to {email}. If you didn't request this OTP, you can safely ignore it.
            </p>
        </div>
    </div>
</body>
</html>";
        }

        public static string GetPasswordResetTemplate(string resetUrl, string userName, string companyName, string supportEmail, string email)
        {
            var greeting = !string.IsNullOrEmpty(userName) ? $"Hi {userName}," : "Hello,";

            return $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Reset Your Password</title>
    <style>
        body {{
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            line-height: 1.6;
            color: #333;
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
            background-color: #f8f9fa;
        }}
        .email-container {{
            background-color: #ffffff;
            border-radius: 10px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
            overflow: hidden;
        }}
        .header {{
            background: linear-gradient(135deg, #e74c3c 0%, #c0392b 100%);
            color: white;
            padding: 40px 30px;
            text-align: center;
        }}
        .header h1 {{
            margin: 0;
            font-size: 28px;
            font-weight: 300;
        }}
        .content {{
            padding: 40px 30px;
        }}
        .greeting {{
            font-size: 18px;
            margin-bottom: 20px;
            color: #2c3e50;
        }}
        .message {{
            font-size: 16px;
            margin-bottom: 30px;
            line-height: 1.8;
        }}
        .cta-button {{
            display: inline-block;
            background: linear-gradient(135deg, #e74c3c 0%, #c0392b 100%);
            color: white;
            text-decoration: none;
            padding: 15px 35px;
            border-radius: 50px;
            font-weight: 600;
            font-size: 16px;
            text-align: center;
            box-shadow: 0 4px 15px rgba(231, 76, 60, 0.3);
            transition: transform 0.2s ease;
        }}
        .cta-button:hover {{
            transform: translateY(-2px);
            box-shadow: 0 6px 20px rgba(231, 76, 60, 0.4);
        }}
        .alternative-link {{
            margin-top: 30px;
            padding: 20px;
            background-color: #f8f9fa;
            border-radius: 8px;
            border-left: 4px solid #e74c3c;
        }}
        .alternative-link p {{
            margin: 0 0 10px 0;
            font-size: 14px;
            color: #6c757d;
        }}
        .alternative-link a {{
            word-break: break-all;
            color: #e74c3c;
            text-decoration: none;
        }}
        .footer {{
            background-color: #f8f9fa;
            padding: 30px;
            text-align: center;
            border-top: 1px solid #e9ecef;
        }}
        .footer p {{
            margin: 5px 0;
            font-size: 14px;
            color: #6c757d;
        }}
        .security-notice {{
            margin-top: 30px;
            padding: 20px;
            background-color: #d1ecf1;
            border: 1px solid #bee5eb;
            border-radius: 8px;
            color: #0c5460;
        }}
        .security-notice strong {{
            color: #155724;
        }}
        .warning-notice {{
            margin-top: 30px;
            padding: 20px;
            background-color: #f8d7da;
            border: 1px solid #f1aeb5;
            border-radius: 8px;
            color: #721c24;
        }}
        .warning-notice strong {{
            color: #721c24;
        }}
        @media (max-width: 600px) {{
            .content {{
                padding: 30px 20px;
            }}
            .header {{
                padding: 30px 20px;
            }}
            .cta-button {{
                display: block;
                margin: 0 auto;
            }}
        }}
    </style>
</head>
<body>
    <div class='email-container'>
        <div class='header'>
            <h1>🔐 Password Reset Request</h1>
        </div>
        
        <div class='content'>
            <div class='greeting'>{greeting}</div>
            
            <div class='message'>
                <p>We received a request to reset the password for your <strong>{companyName}</strong> account.</p>
                <p>If you made this request, click the button below to reset your password:</p>
            </div>
            
            <div style='text-align: center; margin: 40px 0;'>
                <a href='{resetUrl}' class='cta-button'>Reset My Password</a>
            </div>
            
            <div class='alternative-link'>
                <p><strong>Button not working?</strong> Copy and paste this link into your browser:</p>
                <a href='{resetUrl}'>{resetUrl}</a>
            </div>
            
            <div class='security-notice'>
                <p><strong>🔒 Security Information:</strong></p>
                <ul style='margin: 10px 0; padding-left: 20px;'>
                    <li>This reset link will expire in <strong>1 hour</strong> for your security</li>
                    <li>The link can only be used once</li>
                    <li>Your current password remains active until you create a new one</li>
                </ul>
            </div>
            
            <div class='warning-notice'>
                <p><strong>⚠️ Didn't request this?</strong></p>
                <p>If you didn't request a password reset, please ignore this email and your password will remain unchanged. However, if you're concerned about the security of your account, please contact our support team immediately.</p>
            </div>
        </div>
        
        <div class='footer'>
            <p><strong>{companyName}</strong></p>
            <p>Need help? Contact us at <a href='mailto:{supportEmail}' style='color: #e74c3c;'>{supportEmail}</a></p>
            <p style='margin-top: 20px; font-size: 12px;'>
                This email was sent to {email}. If you didn't request this password reset, you can safely ignore it.
            </p>
        </div>
    </div>
</body>
</html>";
        }

        public static string GetPasswordResetSuccessTemplate(string userName, string companyName, string supportEmail, string email)
        {
            var greeting = !string.IsNullOrEmpty(userName) ? $"Hi {userName}," : "Hello,";
            var currentTime = DateTime.UtcNow.ToString("MMMM dd, yyyy 'at' HH:mm UTC");

            return $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Password Reset Successful</title>
    <style>
        body {{
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            line-height: 1.6;
            color: #333;
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
            background-color: #f8f9fa;
        }}
        .email-container {{
            background-color: #ffffff;
            border-radius: 10px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
            overflow: hidden;
        }}
        .header {{
            background: linear-gradient(135deg, #27ae60 0%, #2ecc71 100%);
            color: white;
            padding: 40px 30px;
            text-align: center;
        }}
        .header h1 {{
            margin: 0;
            font-size: 28px;
            font-weight: 300;
        }}
        .content {{
            padding: 40px 30px;
        }}
        .greeting {{
            font-size: 18px;
            margin-bottom: 20px;
            color: #2c3e50;
        }}
        .message {{
            font-size: 16px;
            margin-bottom: 30px;
            line-height: 1.8;
        }}
        .success-icon {{
            text-align: center;
            margin: 30px 0;
        }}
        .success-icon div {{
            width: 80px;
            height: 80px;
            background: linear-gradient(135deg, #27ae60 0%, #2ecc71 100%);
            border-radius: 50%;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            font-size: 40px;
            color: white;
        }}
        .info-box {{
            background-color: #e8f5e8;
            border: 1px solid #c3e6c3;
            border-radius: 8px;
            padding: 20px;
            margin: 30px 0;
            color: #155724;
        }}
        .info-box strong {{
            color: #0f5132;
        }}
        .security-tips {{
            background-color: #f8f9fa;
            border: 1px solid #dee2e6;
            border-radius: 8px;
            padding: 20px;
            margin: 30px 0;
        }}
        .security-tips h3 {{
            margin-top: 0;
            color: #495057;
        }}
        .security-tips ul {{
            margin: 15px 0;
            padding-left: 20px;
        }}
        .security-tips li {{
            margin: 8px 0;
            color: #6c757d;
        }}
        .footer {{
            background-color: #f8f9fa;
            padding: 30px;
            text-align: center;
            border-top: 1px solid #e9ecef;
        }}
        .footer p {{
            margin: 5px 0;
            font-size: 14px;
            color: #6c757d;
        }}
        @media (max-width: 600px) {{
            .content {{
                padding: 30px 20px;
            }}
            .header {{
                padding: 30px 20px;
            }}
        }}
    </style>
</head>
<body>
    <div class='email-container'>
        <div class='header'>
            <h1>✅ Password Reset Successful</h1>
        </div>
        
        <div class='content'>
            <div class='success-icon'>
                <div>✓</div>
            </div>
            
            <div class='greeting'>{greeting}</div>
            
            <div class='message'>
                <p>Great news! Your password has been successfully reset for your <strong>{companyName}</strong> account.</p>
                <p>You can now log in using your new password.</p>
            </div>
            
            <div class='info-box'>
                <p><strong>Reset Details:</strong></p>
                <ul style='margin: 10px 0; padding-left: 20px;'>
                    <li><strong>Date & Time:</strong> {currentTime}</li>
                    <li><strong>Account:</strong> This email address</li>
                    <li><strong>Action:</strong> Password successfully changed</li>
                </ul>
            </div>
            
            <div class='security-tips'>
                <h3>🔒 Security Tips</h3>
                <ul>
                    <li>Keep your password secure and don't share it with anyone</li>
                    <li>Use a unique password that you don't use for other accounts</li>
                    <li>Consider using a password manager to generate and store strong passwords</li>
                    <li>Enable two-factor authentication if available for extra security</li>
                </ul>
            </div>
            
            <div class='message'>
                <p><strong>Didn't reset your password?</strong> If you didn't make this change, please contact our support team immediately at <a href='mailto:{supportEmail}' style='color: #e74c3c;'>{supportEmail}</a>.</p>
            </div>
        </div>
        
        <div class='footer'>
            <p><strong>{companyName}</strong></p>
            <p>Need help? Contact us at <a href='mailto:{supportEmail}' style='color: #27ae60;'>{supportEmail}</a></p>
            <p style='margin-top: 20px; font-size: 12px;'>
                This confirmation was sent to {email} for security purposes.
            </p>
        </div>
    </div>
</body>
</html>";
        }
    }
}

