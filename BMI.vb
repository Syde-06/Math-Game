#Region  Project Attributes 
	#ApplicationLabel: B4A Example
	#VersionCode: 1
	#VersionName: 
	'SupportedOrientations possible values: unspecified, landscape or portrait.
	#SupportedOrientations: unspecified
	#CanInstallToExternalStorage: False
#End Region

#Region  Activity Attributes 
	#FullScreen: True
	#IncludeTitle: False
#End Region

Sub Process_Globals
	'These global variables will be declared once when the application starts.
	'These variables can be accessed from all modules.
	Private xui As XUI
	Dim BMI As Double
End Sub

Sub Globals
	'These global variables will be redeclared each time the activity is created.
	Private Label3 As Label
	Private Label2 As Label
	Private Button1 As Button
	Private EditText2 As EditText
	Private EditText1 As EditText
	Private Label4 As Label
	Private Label5 As Label
	Private Label6 As Label
End Sub

Sub Activity_Create(FirstTime As Boolean)
	Activity.LoadLayout("Layout")
	
End Sub

Sub Activity_Resume

End Sub

Sub Activity_Pause (UserClosed As Boolean)

End Sub

Sub Button1_Click
	' Check for empty fields
	If EditText1.Text.Trim.Length = 0 Or EditText2.Text.Trim.Length = 0 Then
		ToastMessageShow("Please enter both height and weight.", True)
		Return
	End If
	
	' Check for negative numbers
	If EditText1.Text.StartsWith("-") Or EditText2.Text.StartsWith("-") Then
		ToastMessageShow("Both height and weight must be positive numbers", True)
		Return
	End If
	
	' Check if inputs are valid numbers
	If IsNumber(EditText1.Text) = False Or IsNumber(EditText2.Text) = False Then
		ToastMessageShow("Both height and weight must be valid numbers", True)
		Return
	End If
	
	' Check if numbers are not zero
	Dim HeigthVal As Double = EditText1.Text
	Dim WeightVal As Double = EditText2.Text
	
	If HeigthVal <= 0 Or WeightVal <= 0 Then
		ToastMessageShow("Height and weight must be greater than zero", True)
		Return
	End If

	' Calculate BMI
	BMI = WeightVal / ((HeigthVal / 100) * (HeigthVal / 100))
	Dim FormattedBMI As String = NumberFormat(BMI, 1, 2)
	Label2.Text = "Your BMI: " & FormattedBMI

	If BMI < 18.5 Then
		Label3.Text = "Underweight"
		Label3.TextColor = Colors.RGB(52, 152, 219) 'Blue
		Label6.Text = "Consider consulting a nutritionist for healthy weight gain"
	Else If BMI >= 18.5 And BMI < 25 Then
		Label3.Text = "Normal weight"
		Label3.TextColor = Colors.RGB(46, 204, 113) 'Green
		Label6.Text = "Maintain your current healthy lifestyle"
	Else If BMI >= 25 And BMI < 30 Then
		Label3.Text = "Overweight"
		Label3.TextColor = Colors.RGB(241, 196, 15) 'Yellow
		Label6.Text = "Regular exercise and balanced diet recommended"
	Else
		Label3.Text = "Obese"
		Label3.TextColor = Colors.RGB(231, 76, 60) 'Red
		Label6.Text = "Consult healthcare provider for guidance"
	End If
End Sub

Private Sub EditText1_FocusChanged (HasFocus As Boolean)
	If HasFocus = True Then
		Label4.Height = 30dip
	Else if HasFocus = False And EditText1.Text.Length > 0 Then
		Label4.Height = 30dip
	Else
		Label4.Height = 60dip
	End If
End Sub



Private Sub EditText2_FocusChanged (HasFocus As Boolean)
	If HasFocus = True Then
		Label5.Height = 30dip
	Else if HasFocus = False And EditText2.Text.Length > 0 Then
		Label5.Height = 30dip
	Else
		Label5.Height = 60dip
	End If
End Sub

Private Sub Button2_Click
	EditText1.Text = ""
	EditText2.Text = ""
	Label3.TextColor = Colors.RGB(255,255,255) 'Black
	Label3.Text = "..."
	Label2.Text = "Your BMI:"
	Label5.Height = 60dip
	Label4.Height = 60dip
End Sub
