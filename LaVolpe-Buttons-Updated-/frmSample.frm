VERSION 5.00
Begin VB.Form frmSample 
   Caption         =   "Sample for PSC - Vertical Button Version coming soon."
   ClientHeight    =   5610
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   7620
   BeginProperty Font 
      Name            =   "Times New Roman"
      Size            =   8.25
      Charset         =   0
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   LinkTopic       =   "Form1"
   ScaleHeight     =   374
   ScaleMode       =   3  'Pixel
   ScaleWidth      =   508
   StartUpPosition =   3  'Windows Default
   Begin VB.OptionButton OptMode 
      Caption         =   "Above 3 are command buttons"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FF0000&
      Height          =   210
      Index           =   2
      Left            =   4650
      TabIndex        =   14
      Top             =   2700
      Value           =   -1  'True
      Width           =   2610
   End
   Begin VB.OptionButton OptMode 
      Caption         =   "Above 3 option buttons"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FF0000&
      Height          =   210
      Index           =   1
      Left            =   2505
      TabIndex        =   13
      Top             =   2700
      Width           =   2025
   End
   Begin VB.OptionButton OptMode 
      Caption         =   "Above 3 check boxes"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FF0000&
      Height          =   210
      Index           =   0
      Left            =   555
      TabIndex        =   12
      Top             =   2700
      Width           =   1965
   End
   Begin prjSampleButtons.lvButtons_H lvButtons1 
      Height          =   885
      Index           =   2
      Left            =   4740
      TabIndex        =   10
      Tag             =   "1005 x 870"
      ToolTipText     =   "Style: lv_Custom3DBorder"
      Top             =   1590
      Width           =   885
      _ExtentX        =   1561
      _ExtentY        =   1561
      Caption         =   "La ||Volpe "
      CapAlign        =   2
      BackStyle       =   4
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Times New Roman"
         Size            =   9
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      cBhover         =   12632319
      Focus           =   0   'False
      LockHover       =   1
      CapStyle        =   2
      Mode            =   0
      Value           =   0   'False
      CustomClick     =   4
      ImgSize         =   48
      cBack           =   -2147483633
   End
   Begin prjSampleButtons.lvButtons_H lvButtons1 
      Height          =   780
      Index           =   1
      Left            =   1920
      TabIndex        =   9
      ToolTipText     =   "Style: lv_CustomFlat"
      Top             =   1605
      Width           =   1500
      _ExtentX        =   2646
      _ExtentY        =   1376
      Caption         =   "Pats  ||"
      CapAlign        =   1
      BackStyle       =   2
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   9
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      cFore           =   12582912
      cFHover         =   12582912
      cBhover         =   16776960
      LockHover       =   1
      cGradient       =   0
      CapStyle        =   1
      Mode            =   0
      Value           =   0   'False
      ImgSize         =   48
      cBack           =   -2147483633
   End
   Begin prjSampleButtons.lvButtons_H lvSample 
      Height          =   840
      Index           =   3
      Left            =   3120
      TabIndex        =   18
      Top             =   4530
      Width           =   1440
      _ExtentX        =   2540
      _ExtentY        =   1482
      Caption         =   "Round"
      CapAlign        =   2
      BackStyle       =   2
      Shape           =   4
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      cFore           =   0
      cFHover         =   0
      cGradient       =   16777088
      Mode            =   0
      Value           =   0   'False
      ImgAlign        =   3
      Image           =   "frmSample.frx":0000
      ImgSize         =   32
      cBack           =   -2147483633
   End
   Begin VB.CheckBox chkGradients 
      Alignment       =   1  'Right Justify
      Caption         =   "Use gradeint colors?"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   5370
      TabIndex        =   23
      Top             =   4860
      Width           =   1860
   End
   Begin VB.CheckBox chkHover 
      Alignment       =   1  'Right Justify
      Caption         =   "Highlight hover with other color?"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   4545
      TabIndex        =   24
      Top             =   5205
      Width           =   2685
   End
   Begin VB.CheckBox chkDisable 
      Alignment       =   1  'Right Justify
      Caption         =   "Disable above three buttons?"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   4740
      TabIndex        =   22
      Top             =   4530
      Width           =   2490
   End
   Begin VB.ComboBox cboSize 
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      ItemData        =   "frmSample.frx":0452
      Left            =   270
      List            =   "frmSample.frx":0465
      Style           =   2  'Dropdown List
      TabIndex        =   21
      Tag             =   "Primer"
      Top             =   5160
      Width           =   2655
   End
   Begin VB.ComboBox cboAlign 
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      ItemData        =   "frmSample.frx":04C0
      Left            =   270
      List            =   "frmSample.frx":04CD
      Style           =   2  'Dropdown List
      TabIndex        =   19
      Tag             =   "Primer"
      Top             =   4440
      Width           =   2655
   End
   Begin VB.ComboBox cboImgAlign 
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      ItemData        =   "frmSample.frx":0508
      Left            =   270
      List            =   "frmSample.frx":051E
      Style           =   2  'Dropdown List
      TabIndex        =   20
      Top             =   4815
      Width           =   2655
   End
   Begin prjSampleButtons.lvButtons_H lvSample 
      Height          =   1050
      Index           =   0
      Left            =   255
      TabIndex        =   15
      Top             =   3330
      Width           =   2910
      _ExtentX        =   5133
      _ExtentY        =   1852
      Caption         =   "Win XP"
      CapAlign        =   2
      BackStyle       =   2
      Shape           =   2
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Arial"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      cFore           =   0
      cFHover         =   0
      cGradient       =   16777088
      CapStyle        =   1
      Mode            =   0
      Value           =   0   'False
      ImgAlign        =   1
      Image           =   "frmSample.frx":059B
      ImgSize         =   32
      cBack           =   -2147483633
   End
   Begin prjSampleButtons.lvButtons_H lvSource 
      Height          =   645
      Index           =   0
      Left            =   375
      TabIndex        =   0
      Tag             =   "Win XP"
      Top             =   195
      Width           =   1545
      _ExtentX        =   2725
      _ExtentY        =   1138
      Caption         =   "Win &9.x"
      CapAlign        =   2
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      cFore           =   0
      cFHover         =   0
      cGradient       =   0
      Mode            =   0
      Value           =   0   'False
      cBack           =   -2147483633
   End
   Begin prjSampleButtons.lvButtons_H lvSample 
      Height          =   1050
      Index           =   1
      Left            =   2115
      TabIndex        =   16
      Top             =   3330
      Width           =   3375
      _ExtentX        =   5953
      _ExtentY        =   1852
      Caption         =   "Win XP"
      CapAlign        =   2
      BackStyle       =   2
      Shape           =   3
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Arial"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      cFore           =   0
      cFHover         =   0
      cGradient       =   16512
      Mode            =   0
      Value           =   0   'False
      ImgAlign        =   4
      Image           =   "frmSample.frx":09ED
      ImgSize         =   32
      cBack           =   -2147483633
   End
   Begin prjSampleButtons.lvButtons_H lvSample 
      Height          =   1050
      Index           =   2
      Left            =   4440
      TabIndex        =   17
      Top             =   3330
      Width           =   2895
      _ExtentX        =   5106
      _ExtentY        =   1852
      Caption         =   "Win XP"
      CapAlign        =   2
      BackStyle       =   2
      Shape           =   1
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Arial"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      cFore           =   0
      cFHover         =   0
      cGradient       =   16777152
      CapStyle        =   2
      Mode            =   0
      Value           =   0   'False
      ImgAlign        =   3
      Image           =   "frmSample.frx":0E3F
      ImgSize         =   32
      cBack           =   -2147483633
   End
   Begin prjSampleButtons.lvButtons_H lvSource 
      Height          =   645
      Index           =   2
      Left            =   2205
      TabIndex        =   1
      Top             =   195
      Width           =   1545
      _ExtentX        =   2725
      _ExtentY        =   1138
      Caption         =   "Win &XP"
      CapAlign        =   2
      BackStyle       =   2
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      cFore           =   8388608
      cFHover         =   8388608
      cGradient       =   0
      CapStyle        =   2
      Mode            =   0
      Value           =   0   'False
      cBack           =   -2147483633
   End
   Begin prjSampleButtons.lvButtons_H lvSource 
      Height          =   645
      Index           =   3
      Left            =   2205
      TabIndex        =   5
      Top             =   900
      Width           =   1545
      _ExtentX        =   2725
      _ExtentY        =   1138
      Caption         =   "&Java"
      CapAlign        =   2
      BackStyle       =   3
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      cFore           =   8388608
      cFHover         =   8388608
      cGradient       =   0
      Mode            =   0
      Value           =   0   'False
      ImgAlign        =   1
      cBack           =   -2147483633
   End
   Begin prjSampleButtons.lvButtons_H lvSource 
      Height          =   645
      Index           =   4
      Left            =   3990
      TabIndex        =   2
      Top             =   195
      Width           =   1545
      _ExtentX        =   2725
      _ExtentY        =   1138
      Caption         =   "&Flat"
      CapAlign        =   2
      BackStyle       =   4
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      cFore           =   0
      cFHover         =   0
      cGradient       =   0
      Mode            =   0
      Value           =   0   'False
      cBack           =   -2147483633
   End
   Begin prjSampleButtons.lvButtons_H lvSource 
      Height          =   645
      Index           =   6
      Left            =   5760
      TabIndex        =   3
      Top             =   195
      Width           =   1545
      _ExtentX        =   2725
      _ExtentY        =   1138
      Caption         =   "&Netscape"
      CapAlign        =   2
      BackStyle       =   6
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      cFore           =   0
      cFHover         =   0
      cGradient       =   192
      Mode            =   0
      Value           =   0   'False
      cBack           =   -2147483633
   End
   Begin prjSampleButtons.lvButtons_H lvButtons1 
      Height          =   780
      Index           =   0
      Left            =   660
      TabIndex        =   8
      TabStop         =   0   'False
      ToolTipText     =   "Style: lv_Round3D"
      Top             =   1590
      Width           =   780
      _ExtentX        =   1376
      _ExtentY        =   1376
      Caption         =   "Yes?"
      CapAlign        =   2
      BackStyle       =   2
      Shape           =   4
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      cFore           =   0
      cFHover         =   0
      cBhover         =   8421376
      cGradient       =   8421376
      Gradient        =   3
      Mode            =   0
      Value           =   0   'False
      cBack           =   16777152
   End
   Begin prjSampleButtons.lvButtons_H lvButtons1 
      Height          =   810
      Index           =   3
      Left            =   6105
      TabIndex        =   11
      ToolTipText     =   "Style: lv_RoundFlat"
      Top             =   1635
      Width           =   870
      _ExtentX        =   1535
      _ExtentY        =   1429
      CapAlign        =   2
      BackStyle       =   2
      Shape           =   6
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      cFore           =   0
      cFHover         =   0
      cBhover         =   65535
      LockHover       =   1
      cGradient       =   8421376
      Mode            =   0
      Value           =   0   'False
      ImgAlign        =   4
      ImgSize         =   32
      cBack           =   16711935
   End
   Begin prjSampleButtons.lvButtons_H lvSource 
      Height          =   645
      Index           =   1
      Left            =   375
      TabIndex        =   4
      Top             =   900
      Width           =   1545
      _ExtentX        =   2725
      _ExtentY        =   1138
      Caption         =   "Win &3.x"
      CapAlign        =   2
      BackStyle       =   1
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      cFore           =   0
      cFHover         =   0
      cGradient       =   0
      Mode            =   0
      Value           =   0   'False
      cBack           =   -2147483633
   End
   Begin prjSampleButtons.lvButtons_H lvSource 
      Height          =   645
      Index           =   5
      Left            =   3990
      TabIndex        =   6
      Top             =   900
      Width           =   1545
      _ExtentX        =   2725
      _ExtentY        =   1138
      Caption         =   "&Hover"
      CapAlign        =   2
      BackStyle       =   5
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      cFore           =   0
      cFHover         =   0
      cGradient       =   0
      Mode            =   0
      Value           =   0   'False
      ImgAlign        =   1
      ImgSize         =   32
      cBack           =   -2147483633
   End
   Begin prjSampleButtons.lvButtons_H lvSource 
      Height          =   645
      Index           =   7
      Left            =   5760
      TabIndex        =   7
      Tag             =   "Win XP"
      Top             =   900
      Width           =   1545
      _ExtentX        =   2725
      _ExtentY        =   1138
      Caption         =   "&Macintosh"
      CapAlign        =   2
      BackStyle       =   7
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      cFore           =   0
      cFHover         =   0
      cGradient       =   255
      Mode            =   0
      Value           =   0   'False
      ImgAlign        =   1
      ImgSize         =   32
      cBack           =   -2147483633
   End
   Begin VB.Image Image1 
      Enabled         =   0   'False
      Height          =   525
      Index           =   2
      Left            =   6705
      Picture         =   "frmSample.frx":1719
      Stretch         =   -1  'True
      Top             =   3915
      Visible         =   0   'False
      Width           =   810
   End
   Begin VB.Image Image1 
      Enabled         =   0   'False
      Height          =   660
      Index           =   1
      Left            =   6825
      Picture         =   "frmSample.frx":1D98
      Stretch         =   -1  'True
      Top             =   3465
      Visible         =   0   'False
      Width           =   690
   End
   Begin VB.Image Image1 
      Enabled         =   0   'False
      Height          =   630
      Index           =   0
      Left            =   6660
      Picture         =   "frmSample.frx":7C5A
      Stretch         =   -1  'True
      Top             =   3120
      Visible         =   0   'False
      Width           =   855
   End
   Begin VB.Label Label4 
      Alignment       =   2  'Center
      Caption         =   "< Custom Buttons >"
      BeginProperty Font 
         Name            =   "Times New Roman"
         Size            =   9
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000C0&
      Height          =   405
      Left            =   3570
      TabIndex        =   28
      Top             =   1815
      Width           =   870
   End
   Begin VB.Label Label3 
      Caption         =   "Also note that hover butttons have a dashed / dotted border only while in design mode."
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   -1  'True
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   270
      Left            =   405
      TabIndex        =   27
      Top             =   -30
      Visible         =   0   'False
      Width           =   6630
   End
   Begin VB.Label Label2 
      AutoSize        =   -1  'True
      Caption         =   "Click above rectangular buttons to see their style as a diagonal shape."
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   195
      Left            =   270
      TabIndex        =   26
      Top             =   3000
      Width           =   4950
   End
   Begin VB.Shape Shape1 
      BorderColor     =   &H00C00000&
      Height          =   2445
      Left            =   120
      Top             =   3090
      Width           =   7410
   End
   Begin VB.Label Label1 
      Caption         =   "Note how gradients on circular button has nice 3D effect"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   270
      Left            =   765
      TabIndex        =   25
      Top             =   2385
      Width           =   4155
   End
End
Attribute VB_Name = "frmSample"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private I, J, K As Integer
Private bDelay As Boolean, bReset As Boolean
Private bGradientMsg As Boolean
Private bRepaintCounter As Long

Private Sub lvSource_Click(Index As Integer)
' Note the use of the DelayDrawing sub throughout sample form...
' As each property is set, the button will redraw.
' By calling this function before & after all properties are set,
' the button will only redraw once.
lvSource(0).Tag = Replace$(lvSource(Index).Caption, "&", "")
For I = 0 To 3
        lvSample(I).DelayDrawing True
        lvButtons1(I).DelayDrawing True
        lvSample(I).ButtonStyle = Index
        lvButtons1(I).ButtonStyle = Index
        If cboSize.ListIndex < 3 Then
            If I < 3 Then lvSample(I).Caption = lvSource(0).Tag
        End If
        lvSample(I).DelayDrawing False
        lvButtons1(I).DelayDrawing False
Next
End Sub

Private Sub cboSize_Click()
If Len(cboSize.Tag) Then
    cboSize.Tag = ""
    Exit Sub
End If
For I = 0 To 3
    lvSample(I).DelayDrawing True
    If cboSize.ListIndex > 2 Then
        lvSample(I).Caption = ""
    Else
        If I = 3 Then
            lvSample(I).Caption = "Round"
        Else
            lvSample(I).Caption = lvSource(0).Tag
        End If
    End If
    lvSample(I).PictureSize = cboSize.ListIndex
    lvSample(I).DelayDrawing False
Next
End Sub

Private Sub chkDisable_Click()
For I = 0 To 3
    lvSample(I).Enabled = (chkDisable.Value = 0)
Next
End Sub

Private Sub chkGradients_Click()
For I = 0 To 3
    lvSample(I).DelayDrawing True
    If chkGradients Then
        If chkHover = 1 Then
            lvSample(I).HoverColorLocks = lv_NoLocks
            lvSample(I).HoverBackColor = &HFFC0C0
            lvSample(I).HoverForeColor = vbBlue
        Else
            ' this property forces HoverBackColor=GradientBackColor if gradients used
            lvSample(I).HoverColorLocks = lv_LockTextandBackColor
        End If
        lvSample(I).BackColor = vbCyan
        lvSample(I).GradientColor = vbBlue '&H808080
        lvSample(I).CaptionStyle = lv_Default
        lvSample(I).GradientMode = lv_Bottom2Top
    Else
        lvSample(I).GradientMode = lv_NoGradient
        ' this propety forces the HoverBackColor=BackColor if no gradients used
        lvSample(I).BackColor = vbButtonFace
        lvSample(I).HoverColorLocks = lv_LockTextandBackColor
        lvSample(I).ForeColor = 0
        lvSample(I).CaptionStyle = Choose(I + 1, lv_Sunken, lv_Default, lv_Raised, lv_Default)
    End If
    lvSample(I).DelayDrawing False
Next
If chkGradients = 0 And chkHover = 1 Then Call chkHover_Click
If Not bGradientMsg Then
    bGradientMsg = True
    MsgBox "With gradients, the shading is based on the height or width of the button, " & vbCrLf & _
        "depending on the direction of the gradient. This means that buttons using " & vbCrLf & _
        "the same BackColor and GradientColor may appear differently if " & vbCrLf & _
        "the buttons are not the same size." & vbCrLf & vbCrLf & _
        "This is not generally an issue, unless you are attempting to make all your unlike-buttons " & vbCrLf & _
        "look the same. In this case, adjust the colors manually.", vbInformation + vbOKOnly, "One-Time Note on Gradient Fills"
End If
End Sub

Private Sub chkHover_Click()
For I = 0 To 3
    lvSample(I).DelayDrawing True
    If chkHover Then
        lvSample(I).HoverBackColor = vbCyan
        lvSample(I).HoverForeColor = vbBlue
    Else
        ' this property forces HoverBackColor=BackColor if no gradients;
        ' otherwise forces HoverBackColor=GradientColor
        lvSample(I).HoverColorLocks = lv_LockTextandBackColor
    End If
    lvSample(I).DelayDrawing False
Next
If chkGradients = 1 Then Call chkGradients_Click
End Sub

Private Sub cboAlign_Click()
If Len(cboAlign.Tag) Then
    cboAlign.Tag = ""
    Exit Sub
End If
If cboAlign.ListIndex < 2 And cboImgAlign.ListIndex > 3 Then
    bDelay = True
    cboImgAlign.ListIndex = Choose(cboAlign.ListIndex + 1, 3, 1)
    bReset = True
End If
For I = 0 To 3
    If bDelay Then lvSample(I).DelayDrawing True
    lvSample(I).CaptionAlign = cboAlign.ListIndex
    If bReset Then lvSample(I).DelayDrawing False
Next
bReset = False
bDelay = False
End Sub

Private Sub cboImgAlign_Click()
If Len(cboImgAlign.Tag) Then
    cboImgAlign.Tag = ""
    Exit Sub
End If
If cboImgAlign.ListIndex > 3 And cboAlign.ListIndex < 2 Then
    bDelay = True
    cboAlign.ListIndex = 2
    bReset = True
End If
For I = 0 To 3
    If bDelay Then lvSample(I).DelayDrawing True
    lvSample(I).PictureAlign = cboImgAlign.ListIndex
    If bReset Then lvSample(I).DelayDrawing False
Next
bReset = False
bDelay = False
End Sub

Private Sub Form_Load()
cboAlign.ListIndex = 2
cboSize.ListIndex = 2
cboImgAlign.ListIndex = 1
' this example just shows that you can set the Custom buttons via code or you can also
' set the Custom button shapes via the user control's Picture property
' NOTE: The only rule for setting via code is that the picture must be set before the shape is set
Set lvButtons1(1).Picture = Image1(2)
Set lvButtons1(2).Picture = Image1(1)
Set lvButtons1(3).Picture = Image1(1)
lvButtons1(2).ButtonShape = lv_Custom3DBorder
lvButtons1(1).ButtonShape = lv_CustomFlat
' Side Effect that happens when the picture alignment is "lv_ScaleUpDown" and Picture Property changes.
' Need to reset text alignment because it is always automatically set to vbCenter in above cases.
lvButtons1(1).CaptionAlign = vbRightJustify
Show
End Sub

Private Sub lvButtons1_MouseOnButton(Index As Integer, OnButton As Boolean)
Select Case Index
Case 3
    ' this event will fire once when the mouse enters the button shape &
    ' again when the mouse leaves the button shape. Using that info, we
    ' can change the button anyway we want while the cursor is on it
    ' for example...
    lvButtons1(Index).DelayDrawing True
    If OnButton Then
        lvButtons1(Index).BackColor = vbCyan
        Set lvButtons1(Index).Picture = Image1(0)
    Else
        lvButtons1(Index).BackColor = &HC000C0
        Set lvButtons1(Index).Picture = Image1(1)
    End If
    lvButtons1(Index).DelayDrawing False
End Select
End Sub

Private Sub OptMode_Click(Index As Integer)
For I = 2 To 0 Step -1
    lvButtons1(I).Mode = Choose(Index + 1, lv_CheckBox, lv_OptionButton, lv_CommandButton)
Next
lvButtons1(2).ShowFocusRect = (Index <> 2)
End Sub
