Imports System.Data.SqlClient

Public Class PALMA036BIS3

    Private nmed As Decimal
    Private codmate As String
    Private nrem As String
    Private contr As String

    Public Sub New(ByVal nmedidor As Decimal, ByVal codmaterial As String, ByVal Nrem As String, ByVal contrato As String)

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()
        propnmed() = nmedidor
        propsap() = codmaterial
        propnrem() = Nrem
        propcontr() = contrato
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub

    Private Sub PALMA036BIS3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        llenarMEDIDOR()
        txtnserie.Text = propnmed()
        txtdesc.Text = descsap(codmate)
    End Sub

    Public Property propsap() As String
        Get
            Return codmate
        End Get
        Set(ByVal value As String)
            codmate = value
        End Set
    End Property

    Public Property propnmed() As Decimal
        Get
            Return nmed
        End Get
        Set(ByVal value As Decimal)
            nmed = value
        End Set
    End Property

    Public Property propnrem() As String
        Get
            Return nrem
        End Get
        Set(ByVal value As String)
            nrem = value
        End Set
    End Property

    Public Property propcontr() As String
        Get
            Return contr
        End Get
        Set(ByVal value As String)
            contr = value
        End Set
    End Property

    Private Function descsap(ByVal codmat As String) As String
        Dim resp As String = ""
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        con1.Open()
        Dim comando1 As New SqlClient.SqlCommand("SELECT DESC_002 FROM M_MATE_002 WHERE CMATE_002 = @D1", con1)
        comando1.Parameters.Add(New SqlParameter("D1", codmat))
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        If lector1.Read.ToString Then
            resp = lector1.GetValue(0)
            resp = codmat + "-" + resp
        End If
        con1.Close()
        Return resp
    End Function

    Private Sub btnactualizar_Click(sender As Object, e As EventArgs) Handles btnactualizar.Click
        ActualizarT_Med_Devo_113(nmed)
        ActualizarD_remi_devo_116(nmed, codmate)
        Dim var As DialogResult = MessageBox.Show("ACTUALIZADO CORRECTAMENTE.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information)
        If (var = DialogResult.OK) Then
            btnactualizar.Enabled = False
        End If
        txtdesc.Text = txtnserie.Text = cmbsap.Text = Nothing
        Close()
    End Sub

    Private Sub llenarMEDIDOR()
        Dim cnn As New SqlConnection(conexion)
        Dim tabla As New DataTable
        Try
            cnn.Open()
            Dim adt As New SqlDataAdapter("SELECT CMATE_002, DESC_002 FROM M_MATE_002 WHERE (CMATE_002 = '400005' OR CMATE_002 = '400015' OR CMATE_002 = '400025' OR CMATE_002 = '400115' OR CMATE_002 = '400125' OR CMATE_002 = '400125' OR CMATE_002 = '400135' OR CMATE_002 = '400145' OR  CMATE_002 = '400165' OR CMATE_002 = '400185' OR CMATE_002 = '400305' OR CMATE_002 = '400315' OR CMATE_002 = '400325' OR CMATE_002 = 'EXGA09') ORDER BY DESC_002", cnn)
            adt.Fill(tabla)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        If tabla.Rows.Count <> 0 Then
            cmbsap.DataSource = tabla
            cmbsap.DisplayMember = "DESC_002"
            cmbsap.ValueMember = "CMATE_002"
            cmbsap.Text = Nothing
        End If
    End Sub

    Private Sub cmbsap_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbsap.SelectedIndexChanged
        If cmbsap.Text <> Nothing Then
            btnactualizar.Enabled = True
        Else
            btnactualizar.Enabled = False
        End If
    End Sub

    Private Sub ActualizarT_Med_Devo_113(nmed As Decimal)
        Dim cnn As SqlConnection = New SqlConnection(conexion)
        cnn.Open()
        Dim cmd As New SqlCommand("UPDATE T_MED_DEVO_113 SET CMATE_113 = @P1 WHERE NSERI_113 = @P2", cnn)
        cmd.Parameters.AddWithValue("P1", cmbsap.SelectedValue)
        cmd.Parameters.AddWithValue("P2", nmed)
        cmd.ExecuteNonQuery()
        cnn.Close()
    End Sub

    Private Sub ActualizarD_remi_devo_116(nmed As Decimal, codmate As String)
        Dim cnn As SqlConnection = New SqlConnection(conexion)
        Try

            If (contr = "TRATAMIENTO DE MEDIDORES") Then
                contr = "00"
            End If
            If (contr = "UTILIZACION") Then
                contr = "01"
            End If
            If (contr = "SEGUIMIENTO DE DEUDA") Then
                contr = "02"
            End If
            If (contr = "GUARDIA - RETEN") Then
                contr = "03"
            End If
            If (contr = "LABORATORIO") Then
                contr = "04"
            End If
            If (contr = "MEDICION") Then
                contr = "05"
            End If

            cnn.Open()

            'ACTUALIZO LA CANTIDAD.
            Dim cmd As New SqlCommand("UPDATE D_REMITO_DEV_116 SET CANT_116 = CANT_116-1 WHERE NREMITO_116 = @P1 AND CODMATE_116 = @P2 AND CONTRA_116 = @P3", cnn)
            cmd.Parameters.AddWithValue("P1", propnrem)
            cmd.Parameters.AddWithValue("P2", codmate)
            cmd.Parameters.AddWithValue("P3", contr)
            cmd.ExecuteNonQuery()

            cnn.Close()

            'VERIFICO LA CANTIDAD
            cnn.Open()

            Dim cmd1 As New SqlCommand("SELECT CANT_116 FROM D_REMITO_DEV_116 WHERE NREMITO_116 = @P1 AND CODMATE_116 = @P2 AND CONTRA_116 = @P3", cnn)
            cmd1.Parameters.AddWithValue("P1", propnrem)
            cmd1.Parameters.AddWithValue("P2", codmate)
            cmd1.Parameters.AddWithValue("P3", contr)
            Dim vari As Double
            vari = cmd1.ExecuteScalar()

            cnn.Close()
            If (vari <> Nothing) Then
                vari = Convert.ToDouble(vari)
            End If
            ''ACTUALIZO EL COD SAP/LO INSERTO SI NO ESTA.
            If vari = 0 Then
                cnn.Open()
                Dim cmd2 As New SqlCommand("INSERT INTO D_REMITO_DEV_116 (CANT_116, NREMITO_116, FECHA_116, CODMATE_116, CONTRA_116, OBS_116) VALUES (@P1,@P2,@P3,@P4,@P5,@P6)", cnn)
                cmd2.Parameters.AddWithValue("P1", 1)
                cmd2.Parameters.AddWithValue("P2", propnrem)
                cmd2.Parameters.AddWithValue("P3", Date.Now.ToShortDateString())
                cmd2.Parameters.AddWithValue("P4", cmbsap.SelectedValue)
                cmd2.Parameters.AddWithValue("P5", propcontr)
                cmd2.Parameters.AddWithValue("P6", "")
                cmd2.ExecuteNonQuery()
                cnn.Close()
            Else
                cnn.Open()
                'VERIFICO LA CANTIDAD DEL COD NUEVO, SI NO HAY NINGUNO CON ESE CODIGO, LO TENGO QUE AGREGAR Y NO ES UN UPDATE.
                Dim cmd4 As New SqlCommand("SELECT CANT_116 FROM D_REMITO_DEV_116 WHERE NREMITO_116 = @P1 AND CODMATE_116 = @P2 AND CONTRA_116 = @P3", cnn)
                cmd4.Parameters.AddWithValue("P1", propnrem)
                cmd4.Parameters.AddWithValue("P2", cmbsap.SelectedValue)
                cmd4.Parameters.AddWithValue("P3", contr)
                Dim variable As Double
                variable = cmd4.ExecuteScalar()
                cnn.Close()
                If (variable <> Nothing) Then
                    variable = Convert.ToDouble(variable)
                End If


                If variable = 0 Then
                    'LO AGREGO
                    cnn.Open()
                    Dim cmd5 As New SqlCommand("INSERT INTO D_REMITO_DEV_116 (CANT_116, NREMITO_116, FECHA_116, CODMATE_116, CONTRA_116, OBS_116) VALUES (@P1,@P2,@P3,@P4,@P5,@P6)", cnn)
                    cmd5.Parameters.AddWithValue("P1", 1)
                    cmd5.Parameters.AddWithValue("P2", propnrem)
                    cmd5.Parameters.AddWithValue("P3", Date.Now.ToShortDateString())
                    cmd5.Parameters.AddWithValue("P4", cmbsap.SelectedValue)
                    cmd5.Parameters.AddWithValue("P5", propcontr)
                    cmd5.Parameters.AddWithValue("P6", "")
                    cmd5.ExecuteNonQuery()
                    cnn.Close()

                Else
                    cnn.Open()
                    'ACTUALIZO PORQUE YA ESTA
                    Dim cmd2 As New SqlCommand("UPDATE D_REMITO_DEV_116 SET CANT_116 = CANT_116+1 WHERE NREMITO_116 = @P1 AND CODMATE_116 = @P2 AND CONTRA_116 = @P3", cnn)
                    cmd2.Parameters.AddWithValue("P1", propnrem)
                    cmd2.Parameters.AddWithValue("P2", cmbsap.SelectedValue)
                    cmd2.Parameters.AddWithValue("P3", propcontr)
                    cmd2.ExecuteNonQuery()
                    cnn.Close()
                End If
            End If


            'VERIFICO LOS SAP QUE QUEDARON CON CANTIDAD 0, Y LOS BORRO SI LOS HAY...
            cnn.Open()
            Dim cmd3 As New SqlCommand("DELETE FROM D_REMITO_DEV_116 WHERE CANT_116 = 0", cnn)
            cmd3.ExecuteNonQuery()
            cnn.Close()

        Catch ex As Exception

        Finally
            cnn.Close()
        End Try
    End Sub

End Class