Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.IO
Imports System.Data.OleDb

Module MAIN
#Region "Cadenas de conexiones"
    '#####  DESTOCK ####
    'Public conexion As String = "Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Bases\BASE_EXGADET.mdf;Integrated Security=True;Connect Timeout=200;User Instance=True"
    ' Public conexionIntegral As String = "Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Bases\Bace_CRC.mdf;Integrated Security=True;Connect Timeout=200;User Instance=True"

    '##### DESTOCK OFICINA ####
    'Public conexion As String = "Data Source=.;AttachDbFilename=C:\Bases\BASE_EXGADET.mdf;Integrated Security=True;Connect Timeout=60;User Instance=True"
    'Public conexionIntegral As String = "Data Source=.;AttachDbFilename=C:\Bases\Bace_CRC.mdf;Integrated Security=True;Connect Timeout=60;User Instance=True"
    '#### clon exgadet
    Public conexion As String = "Data Source=SERVER1;Initial Catalog=BASE_EXGADET_CLON;User ID=sa;Password=Exgadetsa01"
    Public conexionIntegral As String = "Data Source=SERVER1;Initial Catalog=Sistema_Integral_Clon;Persist Security Info=True;User ID=sa;Password=Exgadetsa01"


    '##### EN LA OFICINA ####
    'Public conexion As String = "Data Source=SERVER1;Initial Catalog=BASE_EXGADET;User ID=sa;Password=Exgadetsa01"
    'Public conexionIntegral As String = "Data Source=SERVER1;Initial Catalog=Sistema_Integral_Exgadet;Persist Security Info=True;User ID=sa;Password=Exgadetsa01"
#End Region
#Region "VARIABLES"
    Public _usr As New Clase_Usuario
    Public confirmacion As Boolean
    Public CarpetaCalibracion As String = "‪‪\\SERVER1\CertificadosCalibracion\"
    Public material As New ArrayList
    Public serie As New ArrayList
    '´'Public logo As Image = Image.FromFile("C:\Users\juan jose\Dropbox\Soluciones Integrales M.B\exgadet sin DS.png")
    'Public R As Image = Image.FromFile("C:\Users\juan jose\Dropbox\Soluciones Integrales M.B\R.png")
    Public OC_IMAGEN As Image '= MAIN.ObtenerBitmapdeBDD(1)
    Public REMITO_IMAGEN As Image '= MAIN.ObtenerBitmapdeBDD(2)
    Public mail As String
    Public passmail As String
    Public smtpmail As String
    Public puertomail As Integer
    Public BODY As String
    Public TABLABODY As String
#End Region
    Public Function ContratoMed(ByVal var1 As String) As String
        Dim resp As String = ""
        Dim cnn As SqlConnection = New SqlConnection(conexion)
        'Try
        cnn.Open()
        Dim adt As New SqlCommand("SELECT DESC807 FROM P806_CONTRATO_MED WHERE CCONT807 = @D1", cnn)
        adt.Parameters.Add(New SqlParameter("D1", var1))
        Dim lector As SqlDataReader = adt.ExecuteReader
        If lector.Read Then

            resp = lector.GetValue(0)
        End If
        'Catch ex As Exception
        'MessageBox.Show(ex.Message)
        'Finally
        cnn.Close()
        'End Try
        Return resp
    End Function
    Public Function CodMaterial(ByVal var1 As String) As String
        Dim resp As String = "400015"
        Dim cnn As SqlConnection = New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("select DESC_804 FROM P_CAPACIDAD_804 WHERE CCAP_804 = @D1", cnn)
            adt.Parameters.Add(New SqlParameter("D1", var1))
            Dim lector As SqlDataReader = adt.ExecuteReader
            If lector.Read Then
                resp = lector.GetValue(0)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        Return resp
    End Function
    Public Function obtenerbarras(cod_bar) As String
        Dim COD_25 As String = ""
        Dim cod_barras As String
        Dim codigo_pares As String
        For I = 1 To Len(cod_bar) Step 2
            If CInt(Val(Mid(cod_bar, I, 2))) < 94 Then
                codigo_pares = CInt(Val(Mid(cod_bar, I, 2))) + 33
                COD_25 = COD_25 + Chr(codigo_pares)
            End If
            If CInt(Val(Mid(cod_bar, I, 2))) = 94 Then
                COD_25 = COD_25 + "Ã"
            End If
            If CInt(Val(Mid(cod_bar, I, 2))) = 95 Then
                COD_25 = COD_25 + "Ä"
            End If
            If CInt(Val(Mid(cod_bar, I, 2))) = 96 Then
                COD_25 = COD_25 + "Å"
            End If
            If CInt(Val(Mid(cod_bar, I, 2))) = 97 Then
                COD_25 = COD_25 + "Æ"
            End If
            If CInt(Val(Mid(cod_bar, I, 2))) = 98 Then
                COD_25 = COD_25 + "Ç"
            End If
            If CInt(Val(Mid(cod_bar, I, 2))) = 99 Then
                COD_25 = COD_25 + "È"
            End If
        Next
        cod_barras = "É" + COD_25 + "Ê"
        Return cod_barras
    End Function
    Public Function OBT_NOM_USER(ByVal DNI As String) As String
        Dim RESP As String = ""
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        'Abrimos la conexion
        cnn1.Open()
        'Creamos el comando para crear la consulta
        Dim Comando As SqlCommand = cnn1.CreateCommand
        Comando.CommandText = "select (APELL_001+' ' +NOMB_001) AS NOMBRE from M_USRS_001 WHERE NDOC_001= @USRS"
        'Ejecutamos el commnad y le pasamos el parametro
        Comando.Parameters.Add(New SqlParameter("USRS", DNI))
        Comando.ExecuteNonQuery()
        Dim Dusrs As SqlDataReader = Comando.ExecuteReader
        If Dusrs.Read Then
            RESP = Dusrs.GetValue(0)
        End If
        cnn1.Close()
        Return RESP
    End Function
    Public Function OBT_NOM_PROVE(ByVal DNI As String) As String
        Dim RESP As String = ""
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        'Abrimos la conexion
        cnn1.Open()
        'Creamos el comando para crear la consulta
        Dim Comando As SqlCommand = cnn1.CreateCommand
        Comando.CommandText = "select RAZO_005 from M_PROV_005 WHERE CUIT_005= @USRS"
        'Ejecutamos el commnad y le pasamos el parametro
        Comando.Parameters.Add(New SqlParameter("USRS", DNI))
        Comando.ExecuteNonQuery()
        Dim Dusrs As SqlDataReader = Comando.ExecuteReader
        If Dusrs.Read Then
            RESP = Dusrs.GetValue(0)
        End If
        cnn1.Close()
        Return RESP
    End Function
    Public Sub InsertarFotoEnBDD(ByVal id As Integer, ByVal filefoto As String)
        Try
            Dim ms As MemoryStream = New MemoryStream()
            Dim fs As FileStream = New FileStream(filefoto, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
            ms.SetLength(fs.Length)
            fs.Read(ms.GetBuffer(), 0, fs.Length)
            Dim arrImg() As Byte = ms.GetBuffer()
            ms.Flush()
            fs.Close()
            Using conn As New SqlConnection(conexion)
                MessageBox.Show("juanjo " + arrImg.Length.ToString)
                conn.Open()
                Using cmd As SqlCommand = conn.CreateCommand()
                    cmd.CommandText = "insert into imagenes (id, imagen) values (@id,@imagen)"
                    cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = id
                    cmd.Parameters.Add("@imagen", SqlDbType.VarBinary).Value = arrImg
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Sub ObtenerFotoDeBDD(ByVal id As Integer, ByVal savetofolder As String)
        Try
            Using conn As New SqlConnection(conexion)

                Using cmd As SqlCommand = conn.CreateCommand
                    conn.Open()
                    cmd.CommandText = "select [nombre], [imagen] from IMAGENES where [id]=@id"
                    cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = id
                    Dim reader As SqlDataReader = Nothing
                    reader = cmd.ExecuteReader
                    reader.Read()
                    Dim nombreFicheroBDD As String = reader.Item(0)
                    Dim nSave As String = savetofolder & nombreFicheroBDD
                    Dim arrImg() As Byte = reader.Item(1)
                    Dim ms As MemoryStream = New MemoryStream(arrImg)
                    Dim fs As FileStream = New FileStream(nSave, FileMode.Create, FileAccess.Write, FileShare.ReadWrite)
                    ms.WriteTo(fs)
                    fs.Flush()
                    fs.Close()
                    ms.Close()
                End Using
            End Using
        Catch ex As Exception
            Throw (New Exception(ex.Message))
        End Try
    End Sub
    Public Function ObtenerBitmapdeBDD(ByVal id As Integer) As Image
        Try
            Using conn As New SqlConnection(conexion)
                Using cmd As SqlCommand = conn.CreateCommand
                    conn.Open()
                    cmd.CommandText = "select [imagen] from IMAGENES where [id]=@id"
                    cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = id
                    Dim arrImg() As Byte = cmd.ExecuteScalar
                    Dim ms As New MemoryStream(arrImg)
                    Dim img As Image = Image.FromStream(ms)
                    ms.Close()
                    Return (img)
                End Using
            End Using
        Catch ex As Exception
            Throw (New Exception(ex.Message))
        End Try
    End Function
    Private Sub obtenerimail()
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As SqlCommand = cnn.CreateCommand
            adt.CommandText = "select email, passs,smtp, puerto from mail where id=1"
            Dim lector As SqlDataReader = adt.ExecuteReader
            While lector.Read
                mail = lector.GetValue(0)
                passmail = lector.GetValue(1)
                smtpmail = lector.GetValue(2)
                puertomail = lector.GetInt32(3)
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
    End Sub
    Public Function CargarExcel(ByVal vLibro As String, ByVal vHoja As String) As DataTable
        Dim cs As String = "Provider=Microsoft.Jet.OLEDB.4.0;" & "Data Source=" & vLibro & ";" & "Extended Properties=""Excel 4.0;xml;HDR=YES;IMEX=2"""
        Dim ds As New DataTable
        Try
            Dim cn As New OleDb.OleDbConnection(cs) 'cadena de coneccion 

            If Not System.IO.File.Exists(vLibro) Then
                MsgBox("No se encontro un libro válido en la ubicación especificada.", MsgBoxStyle.Exclamation)
            End If
            Dim da As New OleDbDataAdapter("select * from [" & vHoja & "$]", cs)
            da.Fill(ds)
        Catch ex As Exception
            MsgBox("No se encontro un libro válido en la ubicación especificada. " & ex.Message.ToString, MsgBoxStyle.Exclamation)
        End Try
        Return ds
    End Function
    Public Function ObtMax(ByVal EQUIPO As String, COD As String) As Decimal
        Dim RESP As Decimal = 0
        Dim CNN As New SqlConnection(conexion)
        Try
            CNN.Open()
            Dim ADT As New SqlCommand("SELECT MAX_108 FROM T_SCRI_108 WHERE C_MATE_108 =@D1 AND C_DEPO_108 =@D2", CNN)
            ADT.Parameters.Add(New SqlParameter("D1", COD))
            ADT.Parameters.Add(New SqlParameter("D2", EQUIPO))
            Dim LECTOR As SqlDataReader = ADT.ExecuteReader
            If LECTOR.Read Then
                RESP = LECTOR.GetValue(0)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            CNN.Close()
        End Try
        Return RESP
    End Function
    Public Function CODREZAGO(ByVal COD As String) As String
        Dim RESP As String = "400015"
        Dim CNN As New SqlConnection(conexion)
        Try
            CNN.Open()
            Dim ADT As New SqlCommand("SELECT CODREZA FROM EQUI_REZAGO WHERE CODORIGINAL =@D1", CNN)
            ADT.Parameters.Add(New SqlParameter("D1", COD))
            Dim LECTOR As SqlDataReader = ADT.ExecuteReader
            If LECTOR.Read Then
                RESP = LECTOR.GetValue(0)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            CNN.Close()
        End Try
        Return RESP
    End Function
    Public Function Direcciones_mail(ByVal COD As Integer) As String
        Dim RESP As String = ""
        Dim CNN As New SqlConnection(conexion)
        Try
            CNN.Open()
            Dim ADT As New SqlCommand("SELECT DIRECCION_121 FROM T_MAIL_121 WHERE COD_121 =@D1", CNN)
            ADT.Parameters.Add(New SqlParameter("D1", COD))
            Dim LECTOR As SqlDataReader = ADT.ExecuteReader
            Dim CONT As Integer = 0
            Do While LECTOR.Read
                If CONT = 0 Then
                    RESP = LECTOR.GetValue(0)
                    CONT = 1
                Else
                    RESP = RESP + "," + LECTOR.GetValue(0)
                End If
            Loop
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            CNN.Close()
        End Try
        Return RESP
    End Function
    Public Sub Archivo_conexion(ByVal j As CheckBox, ByVal txt As String)
        If j.Checked = True Then
            conexion = "Data Source=" + txt.ToString + ",1433\SERVER1; Initial Catalog=BASE_EXGADET; Persist Security Info=True;User ID=sa;Password=Exgadetsa01"
            conexionIntegral = "Data Source=" + txt.ToString + ",1433\SERVER1; Initial Catalog=Sistema_Integral_Exgadet; Persist Security Info=True;User ID=sa;Password=Exgadetsa01"
        Else
            '##### CLON
            'conexion = "Data Source=192.168.1.222;Initial Catalog=BASE_EXGADET_CLON;User ID=sa;Password=Exgadetsa01"
            'conexionIntegral = "Data Source=192.168.1.222;Initial Catalog=Sistema_Integral_CLON;Persist Security Info=True;User ID=sa;Password=Exgadetsa01"
            '#### General
            conexion = "Data Source=192.168.1.222;Initial Catalog=BASE_EXGADET;User ID=sa;Password=Exgadetsa01"
            conexionIntegral = "Data Source=192.168.1.222;Initial Catalog=Sistema_Integral_Exgadet;User ID=sa;Password=Exgadetsa01"

            '#### CASA 
            ' conexion = "Data Source=DESKTOP-VNROD0A;Initial Catalog=BASE_EXGADET;User ID=sa;Password=Ortiz01A"
            'conexionIntegral = "Data Source=DESKTOP-VNROD0A;Initial Catalog=Sistema_Integral_Exgadet;User ID=sa;Password=Ortiz01A"

        End If
    End Sub
    Public Sub imagenes()
        OC_IMAGEN = ObtenerBitmapdeBDD(1)
        REMITO_IMAGEN = ObtenerBitmapdeBDD(2)
        obtenerimail()
    End Sub
   
End Module
