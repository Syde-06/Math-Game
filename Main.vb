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
	Dim timer As Timer
	Dim timeRemaining As Int = 30
	Dim score As Int = 0
	Dim streak As Int = 0
	Dim num1, num2 As Int
	Dim currentOpe As String
	Dim correctAns As Int
End Sub

Sub Globals
	'These global variables will be redeclared each time the activity is created.
	Private Button1 As Button
	Private Button2 As Button
	Private Button3 As Button
	Private Button4 As Button
	Private Button5 As Button
	Private ScoreLbl As Label
	Private StreakLbl As Label
	Private probLbl As Label
	Private ImageView1 As ImageView
	Private compliLbl As Label
	Private Label2 As Label
End Sub

Sub Activity_Create(FirstTime As Boolean)
	Activity.LoadLayout("Layout")
	IntializeGame
End Sub

Sub Activity_Resume

End Sub

Sub Activity_Pause (UserClosed As Boolean)

End Sub

Sub IntializeGame
	timeRemaining = 30
	score = 0
	ScoreLbl.Text = "Score: " &score
	StreakLbl.Text = "Streak: " &streak
	Label2.Text = "Time Left: " & timeRemaining
	ImageView1.Bitmap = LoadBitmap(File.DirAssets, "default.jpg")
	If timer.IsInitialized Then timer.Enabled = True
	timer.Initialize("timer", 1000)
	timer.Enabled = True
	GenProb
End Sub

Sub GenProb
	Dim ope As Int
	ope = Rnd(0,4)
	
	Select ope
		Case 0
			currentOpe = "+"
			num1 = Rnd(0,200)
			num2 = Rnd(0,200)
			correctAns = num1+num2
		Case 1
			currentOpe = "-"
			num1 = Rnd(0,200)
			num2 = Rnd(0,200)
			If num1 < num2 Then
				Dim temp As Int = num1
				num1 = num2
				num2 = temp
			End If
			correctAns = num1-num2
		Case 2
			currentOpe = "*"
			num1 = Rnd(0,11)
			num2 = Rnd(0,11)
			correctAns = num1*num2
		Case 3
			currentOpe = "/"
			num2 = Rnd(1,11)
			correctAns = Rnd(1,11)
			num1 = num2 * correctAns
	End Select
	
	probLbl.Text = num1 & " " & currentOpe & " " & num2 & " = ?"
	GenAnsOpt
End Sub

Sub GenAnsOpt
	Dim answers As List
	answers.Initialize
	
	answers.Add(correctAns)
	
	Dim attempts As Int = 0
	Dim maxAttempts As Int = 20
	
	Do While answers.Size < 2
		Dim newAnswer As Int = GenWrongAns(correctAns)
		If answers.IndexOf(newAnswer) = -1 Then
			answers.Add(newAnswer)
		End If
		attempts = attempts + 1
		If attempts > maxAttempts Then Exit
	Loop
	
	attempts = 0
	Do While answers.Size < 3
		Dim newAnswer As Int = GenWrongAns(correctAns)
		If answers.IndexOf(newAnswer) = -1 Then
			answers.Add(newAnswer)
		End If
		attempts = attempts + 1
		If attempts > maxAttempts Then Exit
	Loop

	attempts = 0
	Do While answers.Size < 4
		Dim newAnswer As Int = GenWrongAns(correctAns)
		If answers.IndexOf(newAnswer) = -1 Then
			answers.Add(newAnswer)
		End If
		attempts = attempts + 1
		If attempts > maxAttempts Then Exit
	Loop
	
	For i = answers.Size - 1 To 0 Step -1
		Dim j As Int = Rnd(0, i + 1)
		Dim temp As Object = answers.Get(i)
		answers.Set(i, answers.Get(j))
		answers.Set(j, temp)
	Next
	
	Button1.Text = answers.Get(0)
	Button2.Text = answers.Get(1)
	Button3.Text = answers.Get(2)
	Button4.Text = answers.Get(3)
End Sub

Sub GenWrongAns(correctAnswer As Int) As Int
	Dim wrongAnswer As Int
	Dim variation As Int = Rnd(1, 6)
    
	If Rnd(0, 2) = 0 Then
		wrongAnswer = correctAnswer + variation
	Else
		wrongAnswer = correctAnswer - variation
	End If

	If wrongAnswer <= 0 Then
		wrongAnswer = correctAnswer + variation + 1
	End If
    
	Return wrongAnswer
End Sub

Sub BtnEnable(val As Boolean)
	Button1.Enabled = val
	Button2.Enabled = val
	Button3.Enabled = val
	Button4.Enabled = val
	Button5.Enabled = val
End Sub

Sub CheckAns(selectedAns As Int)
	timer.Enabled = False
	BtnEnable(False)
	If selectedAns = correctAns Then
		ImageView1.Bitmap = LoadBitmap(File.DirAssets, "Correct.jpg")
		score = score + Max(5,5+streak)
		streak = streak + 1
		ScoreLbl.Text = "Score: " &score
		StreakLbl.Text = "Streak: " &streak
		compliLbl.TextColor = Colors.Green
		compliLbl.Text = "✓ Correct!"
		If score>= 25 Then
			compliLbl.Text = "WoW!"
		Else If score >= 15 Then
			compliLbl.Text = "Nice"
		End If
	Else
		score = score - 5
		If score < 0 Then score = 0
		streak = 0
		ScoreLbl.Text = "Score: " &score
		StreakLbl.Text = "Streak: " &streak
		compliLbl.TextColor = Colors.Red
		If selectedAns <> correctAns Then
			compliLbl.Text = "✗ Wrong!"
		Else If selectedAns < correctAns Then
			compliLbl.Text = "Too Low"
		Else
			compliLbl.Text = "Too High"
		End If
		If File.Exists(File.DirAssets, "wrong.jpg") Then
			ImageView1.Bitmap = LoadBitmap(File.DirAssets, "wrong.jpg")
		End If
		If Abs(selectedAns - correctAns) > 10 Then
			compliLbl.Text = "???"
		End If
	End If
	Dim resetTimer As Timer
	resetTimer.Initialize("resetTimer", 2000)
	resetTimer.Enabled = True
End Sub

Sub resetTimer_Tick
	Dim t As Timer = Sender
	t.Enabled = False
	GenProb
	timer.Enabled = True
	compliLbl.Text = ""
	ImageView1.Bitmap = LoadBitmap(File.DirAssets, "default.jpg")
	timeRemaining = 30
	Label2.TextColor = Colors.Black
	Label2.Text = "Time Left: " & timeRemaining
	BtnEnable(True)
End Sub

Sub timer_Tick
	timeRemaining = timeRemaining - 1
	Label2.Text = "Time Left: " & timeRemaining

	If timeRemaining <= 10 Then
		Label2.TextColor = Colors.Red
	Else If timeRemaining <= 20 Then
		Label2.TextColor = Colors.Yellow
	Else
		Label2.TextColor = Colors.Black
	End If
    
	If timeRemaining <= 0 Then
		timer.Enabled = False
      	Label2.Text = "Time's UP!"
		If File.Exists(File.DirAssets, "timeup.jpg") Then
			ImageView1.Bitmap = LoadBitmap(File.DirAssets, "timeup.jpg")
		End If
		Dim timeUpTimer As Timer
		timeUpTimer.Initialize("timeUpTimer", 3000)
		timeUpTimer.Enabled = True
	End If
End Sub

Sub timeUpTimer_Tick
	Dim t As Timer = Sender
	t.Enabled = False
	timeRemaining = 30
	GenProb
	timer.Enabled = True
	compliLbl.Text = ""
	Label2.Text = "Time Left: " & timeRemaining
End Sub

Private Sub Button5_Click
	ImageView1.Bitmap = LoadBitmap(File.DirAssets, "default.jpg")
	If timer.IsInitialized Then timer.Enabled = True
	timer.Initialize("timer", 1000)
	timer.Enabled = True
	GenProb
End Sub

Private Sub Button4_Click
	CheckAns(Button4.Text)
End Sub

Private Sub Button3_Click
	CheckAns(Button3.Text)
End Sub

Private Sub Button2_Click
	CheckAns(Button2.Text)
End Sub

Private Sub Button1_Click
	CheckAns(Button1.Text)
End Sub
