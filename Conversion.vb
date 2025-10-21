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
	Dim Updating As Boolean
End Sub

Sub Globals
	'These global variables will be redeclared each time the activity is created.
	Private Button1 As Button
	Private PoundsVal As EditText
	Private KgVal As EditText
	Private CMVal As EditText
	Private MetersVal As EditText
	Private FeetVal As EditText
	Private InchesVal As EditText
End Sub

Sub Activity_Create(FirstTime As Boolean)
	Activity.LoadLayout("Layout")
End Sub

Sub Activity_Resume

End Sub

Sub Activity_Pause (UserClosed As Boolean)

End Sub



Private Sub Button1_Click
	Updating = True
	InchesVal.Text = ""
	FeetVal.Text = ""
	MetersVal.Text = ""
	CMVal.Text = ""
	KgVal.Text = ""
	PoundsVal.Text = ""
	Updating = False
End Sub

Private Sub InchesVal_TextChanged (Old As String, New As String)
	If Updating Then Return
	Ft2Inch
End Sub

Private Sub FeetVal_TextChanged (Old As String, New As String)
	If Updating Then Return
	Ft2Inch
End Sub

Private Sub MetersVal_TextChanged (Old As String, New As String)
	If Updating Then Return
	ConvMeters
End Sub

Private Sub CMVal_TextChanged (Old As String, New As String)
	If Updating Then Return
	ConvCenti
End Sub

Private Sub KgVal_TextChanged (Old As String, New As String)
	If Updating Then Return
	Kg2Pounds
End Sub

Private Sub PoundsVal_TextChanged (Old As String, New As String)
	If Updating Then Return
	Pounds2Kg
End Sub

Sub Pounds2Kg
	Try
		If PoundsVal.Text = "" Then
			Updating = True
			KgVal.Text = ""
			Updating = False
		End If
		
		Dim pounds As Double = PoundsVal.Text
		Dim Kg As Double = pounds * 0.453592
		
		Updating = True
		KgVal.Text = NumberFormat(Kg, 1, 2)
		Updating = False
	Catch
		Log(LastException)
	End Try
End Sub

Sub Kg2Pounds
	Try
		If KgVal.Text = "" Then
			Updating = True
			PoundsVal.Text = ""
			Updating = False
		End If
		
		Dim Kg As Double = KgVal.Text
		Dim pounds As Double = Kg * 2.20462
		
		Updating = True
		PoundsVal.Text = NumberFormat(pounds, 1, 2)
		Updating = False
	Catch
		Log(LastException)
	End Try
End Sub

Sub Ft2Inch
	Try
		Dim feet As Double = 0
		Dim inches As Double = 0
        
		If FeetVal.Text <> "" Then feet = FeetVal.Text
		If InchesVal.Text <> "" Then inches = InchesVal.Text
        
		' Convert to centimeters (1 foot = 30.48 cm, 1 inch = 2.54 cm)
		Dim totalCm As Double = (feet * 30.48) + (inches * 2.54)
        
		Updating = True
		CMVal.Text = NumberFormat2(totalCm, 1, 2, 2, False)
		MetersVal.Text = NumberFormat2(totalCm / 100, 1, 2, 2, False)
		Updating = False
	Catch
		Log(LastException)
	End Try
End Sub

Sub ConvCenti
	Try
		If CMVal.Text = "" Then
			Updating = True
			FeetVal.Text = ""
			InchesVal.Text = ""
			MetersVal.Text = ""
			Updating = False
			Return
		End If
        
		Dim cm As Double = CMVal.Text
        
		' Convert to feet and inches
		Dim totalInches As Double = cm / 2.54
		Dim feet As Int = Floor(totalInches / 12)
		Dim inches As Double = totalInches Mod 12
        
		Updating = True
		FeetVal.Text = feet
		InchesVal.Text = NumberFormat2(inches, 1, 2, 2, False)
		MetersVal.Text = NumberFormat2(cm / 100, 1, 2, 2, False)
		Updating = False
	Catch
		Log(LastException)
	End Try
End Sub

Sub ConvMeters
	Try
		If MetersVal.Text = "" Then
			Updating = True
			FeetVal.Text = ""
			InchesVal.Text = ""
			CMVal.Text = ""
			Updating = False
			Return
		End If
        
		Dim meters As Double = MetersVal.Text
		Dim cm As Double = meters * 100
        
		' Convert to feet and inches
		Dim totalInches As Double = cm / 2.54
		Dim feet As Int = Floor(totalInches / 12)
		Dim inches As Double = totalInches Mod 12
        
		Updating = True
		FeetVal.Text = feet
		InchesVal.Text = NumberFormat2(inches, 1, 2, 2, False)
		CMVal.Text = NumberFormat2(cm, 1, 2, 2, False)
		Updating = False
	Catch
		Log(LastException)
	End Try
End Sub
