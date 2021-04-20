Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.IO
Public Class PCOMP003
    Private oc As New Class_OC
    Private mensaje As New Clase_mensaje
    Private metodos As New Clas_Almacen
    Private NESTADO As Integer
    Private _FECHA As Date
    Private _NPETICION As Decimal
    Private _FAPROBADA As Date
    Private _PROVEEDOR As String
    Private _CONTRATO As String


   

    Private Sub PCOMP003_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        llenarDW1()
    End Sub

    Private Sub llenarDW1()
        Dim d1 As String = ""
        Dim d2 As String = ""
        Dim d3 As String = ""
        Dim d4 As String = ""
        Dim d5 As String = ""
        Dim d6 As String = ""
        Dim d7 As String = ""
        Dim d8 As String = ""
        DGV1.Rows.Clear()
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT T_C_OC_105.N_OC_105, T_C_OC_105.F_ALTA_105, T_C_OC_105.FAPRO_105, M_PROV_005.RAZO_005, T_C_OC_105.USERG_105, T_C_OC_105.USERR_105, dbo.T_C_OC_105.CONPRECIO_105,MONTO_105 FROM dbo.T_C_OC_105 INNER JOIN M_PROV_005 ON T_C_OC_105.C_PROV_105 = M_PROV_005.CUIT_005 WHERE (T_C_OC_105.TIPO_OC_105 = 1) AND (T_C_OC_105.ESTA_105 = 3) and (T_C_OC_105.FIMPRE_105 IS NULL)", con1)
        'creo el lector de parametros

        comando1.ExecuteNonQuery()
        'genero un lector
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        While lector1.Read
            d1 = lector1.GetValue(0).ToString.PadLeft(8, "0")
            d2 = lector1.GetDateTime(1).ToShortDateString
            d3 = MAIN.OBT_NOM_USER(lector1.GetValue(4))
            If IsDBNull(lector1.GetValue(2)) = False Then
                d4 = lector1.GetDateTime(2).ToShortDateString
            End If
            If IsDBNull(lector1.GetValue(5)) = False Then
                d5 = MAIN.OBT_NOM_USER(lector1.GetValue(5))
            Else
                d5 = "NO APROBADA"
            End If
            d6 = lector1.GetValue(3)
            If IsDBNull(lector1.GetValue(6)) Then
                d7 = "SIN VALOR"
            Else
                If lector1.GetValue(6) = "0" Then
                    d7 = "SIN VALOR"
                Else
                    d7 = "CON VALOR"
                End If
            End If
            If IsDBNull(lector1.GetValue(7)) Then
                d8 = "0"
            Else
                d8 = lector1.GetValue(7).ToString
            End If



            Me.DGV1.Rows.Add(d1, d2, d3, d4, d5, d6, d7, d8)
        End While
        Me.DGV2.Rows.Clear()
        'ciero la conexion
        con1.Close()
    End Sub
    Private Sub DataGridView1_DoubleClick1(ByVal sender As Object, ByVal e As System.EventArgs) Handles DGV1.DoubleClick
        If DGV1.RowCount <> 0 Then
            llenarDW2_estado2y3(CDec(Me.DGV1.Item(0, DGV1.CurrentRow.Index).Value))
            _NPETICION = Me.DGV1.Item(0, DGV1.CurrentRow.Index).Value
            _FECHA = Me.DGV1.Item(1, DGV1.CurrentRow.Index).Value
            _FAPROBADA = Me.DGV1.Item(3, DGV1.CurrentRow.Index).Value
            _PROVEEDOR = Me.DGV1.Item(5, DGV1.CurrentRow.Index).Value
            _CONTRATO = Me.DGV1.Item(6, DGV1.CurrentRow.Index).Value
            If DGV2.RowCount <> 0 Then
                Button3.Enabled = True
            End If
        End If
    End Sub
    Private Sub llenarDW2_estado2y3(ByVal nremi As Decimal)
        Dim mate As String = ""
        Dim desc As String = ""
        Dim soli As Decimal = 0
        Dim ent As Decimal = 0
        Dim U As String
        Dim precio As Decimal = 0
        Dim total As Decimal = 0
        DGV2.Rows.Clear()
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT dbo.T_D_OC_106.C_MATE_106, dbo.M_MATE_002.DESC_002, dbo.M_MATE_002.UNID_002, dbo.T_D_OC_106.CANT_106, dbo.T_D_OC_106.CANTE_106,dbo.T_D_OC_106.PRECIO_C_106 FROM dbo.T_D_OC_106 INNER JOIN dbo.M_MATE_002 ON dbo.T_D_OC_106.C_MATE_106 = dbo.M_MATE_002.CMATE_002 WHERE (dbo.T_D_OC_106.N_OC_106 = @D1) and (dbo.T_D_OC_106.CONF_106 = 1) ", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", nremi))
        comando1.ExecuteNonQuery()
        'genero un lector
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        'pregunto si encontro
        While lector1.Read
            mate = lector1.GetValue(0)
            desc = lector1.GetValue(1)
            soli = lector1.GetValue(3)
            ent = lector1.GetValue(4)
            U = lector1.GetValue(2)
            precio = lector1.GetValue(5)
            total = soli * precio
            Me.DGV2.Rows.Add(mate, desc, U, soli, ent, soli - ent, precio, total)
        End While
        'ciero la conexion
        con1.Close()
    End Sub







    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs)
        '#######################datos##################################
        Dim R1_T1 As String = ""
        Dim R1_T2 As String = ""
        Dim R2_T1 As String = ""
        Dim R2_T2 As String = ""
        Dim R3_T1 As String = ""
        Dim R3_T2 As String = ""
        Dim cuit As String = oc.CUIT(_NPETICION).ToString
        Dim DIRECCION As String = oc.Dir_proveedor(cuit).ToString
        Dim n_REMITO As String
        If _usr.Obt_Almacen = 0 Then
            n_REMITO = "0100" + "-" + _NPETICION.ToString.PadLeft(8, "0")
        Else
            n_REMITO = _usr.Obt_Almacen.ToString.PadLeft(4, "0") + "-" + _NPETICION.ToString.PadLeft(8, "0")
        End If        'DEFINO LAS VARIABLES
        R1_T1 = "PROVEEDOR: " + _PROVEEDOR.ToString + "(" + cuit.ToString + ")"
        R1_T2 = "CONTRATO: " + _CONTRATO.ToString
        R2_T1 = "DIRECCION: " + DIRECCION.ToString
        ''R2_T2 = "APROBO: " + oc.QUIEN_APROBO(_NPETICION).ToString + "(" + _FAPROBADA.ToShortDateString + ")"
        R3_T1 = "APROBO: " + oc.QUIEN_APROBO(_NPETICION).ToString + "(" + _FAPROBADA.ToShortDateString + ")"
        R3_T2 = "CONFECCIONO: " + oc.QUIEN_CONFECCIONO(_NPETICION).ToString
        ''R3_T2 = "CANTIDAD DE ITEM: " + DataGridView2.RowCount.ToString

        'DEFINO LA LINEA DEL REMITO Y EL SALTO
        Dim LINEA As Integer = 356
        Dim SALTO As Integer = 24
        'IMAGEN ######################################
        e.Graphics.DrawImage(MAIN.OC_IMAGEN, 0, 0, 800, 1140)
        'ESCRIBO EL REMITO Y LA FECHA
        e.Graphics.DrawString(n_REMITO.ToString, New Font("ARIAL", 16, FontStyle.Bold), Brushes.Black, 435, 73)
        e.Graphics.DrawString(_FECHA.ToString, New Font("ARIAL", 12, FontStyle.Regular), Brushes.Black, 435, 101)
        'ESCRIBO LOS RENGLONES
        e.Graphics.DrawString(R1_T1.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, 215)
        e.Graphics.DrawString(R2_T1.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, 247)
        e.Graphics.DrawString(R3_T1.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, 280)

        e.Graphics.DrawString(R1_T2.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 400, 215)
        e.Graphics.DrawString(R2_T2.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 450, 247)
        e.Graphics.DrawString(R3_T2.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 450, 280)


        e.Graphics.DrawString("CODIGO", New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, 325)
        e.Graphics.DrawString("DESCRIPCION", New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 350, 325)
        e.Graphics.DrawString("U", New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 680, 325)
        e.Graphics.DrawString("CANT", New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 715, 325)
        'RECORRO EL DATA
        For I = 0 To DGV2.RowCount - 1
            e.Graphics.DrawString(Me.DGV2.Item(0, I).Value.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, LINEA)
            e.Graphics.DrawString(Me.DGV2.Item(1, I).Value.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 80, LINEA)
            e.Graphics.DrawString(Me.DGV2.Item(2, I).Value.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 680, LINEA)
            e.Graphics.DrawString(Me.DGV2.Item(3, I).Value.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 730, LINEA)
            LINEA += SALTO
        Next

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        oc.IMPRIMIR_OC(_NPETICION)
        oc.ImprimirOC(_NPETICION)
        Me.DGV1.Rows.Clear()
        Me.DGV2.Rows.Clear()
        llenarDW1()
    End Sub




End Class