Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.IO


Public Class Class_OC
    Public Function Obtener_Numero_OC() As Decimal
        'lee el ultimo numero de remito
        Dim Numero_Remito As Decimal
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("select NUMERO From NUMERACION where C_NUM=2", con1)
        'creo un lector
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        If lector1.Read.ToString() Then
            Numero_Remito = CDec(lector1.GetValue(0))
        End If
        con1.Close()
        Return Numero_Remito
    End Function
    Public Sub Sumar_Num_OC()
        'incrementa el numero de remito
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("Update NUMERACION set NUMERO = NUMERO+1 WHERE C_NUM=2", con1)
        comando1.ExecuteReader()
        con1.Close()
    End Sub
    Public Sub grabar_cab_OC(ByVal _NOC As Decimal, ByVal FALTA As Date, ByVal USER As String, ByVal PROV As String, ByVal TIPO As Integer, ByVal CONTRATO As String)
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("insert T_C_OC_105 (N_OC_105,F_ALTA_105,USERG_105,ESTA_105,C_PROV_105,TIPO_OC_105, CONT_105) values(@D1,@D2,@D3,@D4,@D5,@D6,@D10)", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", _NOC))
        comando1.Parameters.Add(New SqlParameter("D2", FALTA))
        comando1.Parameters.Add(New SqlParameter("D3", USER))
        comando1.Parameters.Add(New SqlParameter("D4", 1))
        comando1.Parameters.Add(New SqlParameter("D5", PROV))
        comando1.Parameters.Add(New SqlParameter("D6", TIPO))
        comando1.Parameters.Add(New SqlParameter("D10", CONTRATO))
        comando1.ExecuteNonQuery()
        con1.Close()
    End Sub
    Public Sub grabar_det_oc(ByVal NOC As Decimal, ByVal MATERIAL As String, ByVal CANTS As Decimal, ByVal CANTE As Decimal, ByVal CONF As Boolean)
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("insert T_D_OC_106 (N_OC_106, C_MATE_106, CANT_106, CANTE_106,CONF_106) values(@D1,@D2,@D3,@D4,@D5)", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", NOC))
        comando1.Parameters.Add(New SqlParameter("D2", MATERIAL))
        comando1.Parameters.Add(New SqlParameter("D3", CANTS))
        comando1.Parameters.Add(New SqlParameter("D4", CANTE))
        comando1.Parameters.Add(New SqlParameter("D5", CONF))
        comando1.ExecuteNonQuery()
        con1.Close()
    End Sub
    Public Sub cerrar_peticion(ByVal npeti As Decimal, ByVal estado As Integer, ByVal FECHA As Date, ByVal USR As String, ByVal MOTI As Integer)
        'incrementa el numero de remito
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("Update T_C_OC_105 set ESTA_105=@E1, FMODI_105 = @E2 , USERM_105=@E3 , MCIERRE_105 = @E4 WHERE N_OC_105=@D1", con1)
        comando1.Parameters.Add(New SqlParameter("E1", estado))
        comando1.Parameters.Add(New SqlParameter("E2", FECHA))
        comando1.Parameters.Add(New SqlParameter("E3", USR))
        comando1.Parameters.Add(New SqlParameter("E4", MOTI))
        comando1.Parameters.Add(New SqlParameter("D1", npeti))
        comando1.ExecuteReader()
        con1.Close()
    End Sub
    Public Function VALIDAR_NPETI(ByVal NPETI As Decimal) As Boolean
        Dim RESPUESTA As Boolean = False
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("select N_OC_105 From T_C_OC_105 where N_PETI_105 = @D1", con1)
        comando1.Parameters.Add(New SqlParameter("D1", NPETI))
        'creo un lector
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        If lector1.Read.ToString() Then
            RESPUESTA = True
        End If
        con1.Close()


        Return RESPUESTA
    End Function
    Public Sub Grabar_N_peticion_gnf(ByVal npeti As Decimal, ByVal estado As Integer, ByVal FECHA As Date, ByVal USR As String, ByVal npetignf As Decimal)
        'incrementa el numero de remito
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("Update T_C_OC_105 set ESTA_105=@E1, FMODI_105 = @E2 , USERM_105=@E3, N_PETI_105 = @E4 WHERE N_OC_105=@D1", con1)
        comando1.Parameters.Add(New SqlParameter("E1", estado))
        comando1.Parameters.Add(New SqlParameter("E2", FECHA))
        comando1.Parameters.Add(New SqlParameter("E3", USR))
        comando1.Parameters.Add(New SqlParameter("E4", npetignf))
        comando1.Parameters.Add(New SqlParameter("D1", npeti))
        comando1.ExecuteReader()
        con1.Close()
    End Sub
    Public Sub Actualizar_cant_entregada(ByVal MATE As String, ByVal npeti As Decimal, ByVal ingresada As Decimal)
        'incrementa el numero de remito
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("Update T_D_OC_106 set CANTE_106=CANTE_106+@E1 WHERE N_OC_106=@D1 and C_MATE_106=@D2", con1)
        comando1.Parameters.Add(New SqlParameter("E1", ingresada))
        comando1.Parameters.Add(New SqlParameter("D1", npeti))
        comando1.Parameters.Add(New SqlParameter("D2", MATE))
        comando1.ExecuteReader()
        con1.Close()
    End Sub
    Public Function Obt_Contrato(ByVal noc As Decimal) As String
        Dim resp As String = ""
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("select cont_105 From T_C_OC_105 where N_OC_105 = @D1", con1)
        comando1.Parameters.Add(New SqlParameter("D1", noc))
        'creo un lector
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        If lector1.Read.ToString() Then
            resp = lector1.GetValue(0)
        End If
        con1.Close()
        Return resp
    End Function
    Public Sub Aprobar_item_OC(ByVal npeti As Decimal, ByVal codmate As String, ByVal apro As String)
        'incrementa el numero de remito
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("Update T_D_OC_106 set CONF_106=@E1 WHERE N_OC_106=@D1 AND C_MATE_106 = @D2", con1)
        comando1.Parameters.Add(New SqlParameter("E1", apro))
        comando1.Parameters.Add(New SqlParameter("D1", npeti))
        comando1.Parameters.Add(New SqlParameter("D2", codmate))
        comando1.ExecuteReader()
        con1.Close()
    End Sub
    Public Sub aprobar_oc(ByVal npeti As Decimal, ByVal estado As Integer, ByVal FECHA As Date, ByVal USR As String)
        'incrementa el numero de remito
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("Update T_C_OC_105 set ESTA_105=@E1, FAPRO_105 = @E2 , USERR_105=@E3 WHERE N_OC_105=@D1", con1)
        comando1.Parameters.Add(New SqlParameter("E1", estado))
        comando1.Parameters.Add(New SqlParameter("E2", FECHA))
        comando1.Parameters.Add(New SqlParameter("E3", USR))
        comando1.Parameters.Add(New SqlParameter("D1", npeti))
        comando1.ExecuteReader()
        con1.Close()
    End Sub
    Public Function QUIEN_APROBO(ByVal NPETI As Decimal) As String
        Dim RESPUESTA As String = ""
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("select USERR_105 From T_C_OC_105 where N_OC_105 = @D1", con1)
        comando1.Parameters.Add(New SqlParameter("D1", NPETI))
        'creo un lector
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        If lector1.Read Then
            RESPUESTA = MAIN.OBT_NOM_USER(lector1.GetValue(0))
        End If
        con1.Close()
        Return RESPUESTA
    End Function
    Public Function QUIEN_CONFECCIONO(ByVal NPETI As Decimal) As String
        Dim RESPUESTA As String = ""
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("select USERG_105 From T_C_OC_105 where N_OC_105 = @D1", con1)
        comando1.Parameters.Add(New SqlParameter("D1", NPETI))
        'creo un lector
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        If lector1.Read Then
            RESPUESTA = MAIN.OBT_NOM_USER(lector1.GetValue(0))
        End If
        con1.Close()
        Return RESPUESTA
    End Function
    Public Function CUIT(ByVal NPETI As Decimal) As String
        Dim RESPUESTA As String = ""
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("select C_PROV_105 From T_C_OC_105 where N_OC_105 = @D1", con1)
        comando1.Parameters.Add(New SqlParameter("D1", NPETI))
        'creo un lector
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        If lector1.Read Then
            RESPUESTA = lector1.GetValue(0)
        End If
        con1.Close()
        Return RESPUESTA
    End Function
    Public Function que_contrato(ByVal cont As String) As String
        Dim RESPUESTA As String = ""
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("select DESC_004 From M_CONT_004 where NCONT_004 = @D1", con1)
        comando1.Parameters.Add(New SqlParameter("D1", cont))
        'creo un lector
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        If lector1.Read Then
            RESPUESTA = lector1.GetValue(0)
        End If
        con1.Close()
        Return RESPUESTA
    End Function
    Public Function Dir_proveedor(ByVal cuit As String) As String
        Dim RESPUESTA As String = ""
        Dim cod_partido As Integer
        Dim cod_loca As Integer
        Dim resp2 As String = ""
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("select DIRE_005, LOCA_005, PART_005 From M_PROV_005 where (CUIT_005 = @D1)", con1)
        comando1.Parameters.Add(New SqlParameter("D1", cuit))
        'creo un lector
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        If lector1.Read Then

            cod_loca = lector1.GetInt32(1)
            cod_partido = lector1.GetInt32(2)
            resp2 = localidad_porv(cod_loca, cod_partido)
            'RESPUESTA = RESPUESTA + " - " + LECTOR2.GetValue(0)
            RESPUESTA = lector1.GetValue(0) + " - " + resp2.ToString

        End If
        con1.Close()
        Return RESPUESTA
    End Function

    Public Sub IMPRIMIR_OC(ByVal npeti As Decimal)
        'incrementa el numero de remito
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("Update T_C_OC_105 set FIMPRE_105= @E2 WHERE N_OC_105=@D1", con1)
        comando1.Parameters.Add(New SqlParameter("E2", Date.Now))
        comando1.Parameters.Add(New SqlParameter("D1", npeti))
        comando1.ExecuteReader()
        con1.Close()
    End Sub
    Public Function verificar_oc_abierta(ByVal material As String, ByVal contrato As String) As Boolean
        'lee el ultimo numero de remito
        Dim conf As Boolean = False
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT T_C_OC_105.N_OC_105, T_D_OC_106.CONF_106 FROM T_C_OC_105 INNER JOIN T_D_OC_106 ON T_C_OC_105.N_OC_105 = T_D_OC_106.N_OC_106 WHERE (T_C_OC_105.ESTA_105 = 3) AND (T_D_OC_106.C_MATE_106 = @D1) AND (T_C_OC_105.CONT_105 = @D2) AND (T_C_OC_105.TIPO_OC_105 = 2) AND (T_D_OC_106.CONF_106 = 1) AND (T_D_OC_106.CANTE_106 <= T_D_OC_106.CANT_106)", con1)
        comando1.Parameters.Add(New SqlParameter("D1", material))
        comando1.Parameters.Add(New SqlParameter("D2", contrato))

        'creo un lector
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        If lector1.Read.ToString() Then
            conf = True
        End If
        con1.Close()
        Return conf
    End Function
    Public Function localidad_porv(ByVal cloca As Integer, ByVal cpart As Integer) As String

        Dim resp As String = ""
        Dim CNN2 As SqlConnection = New SqlConnection(conexion)
        Try



            CNN2.Open()
            Dim COMANDO2 As New SqlClient.SqlCommand("SELECT DESC_802 FROM DET_PARAMETRO_802 WHERE (C_TABLA_802=8) AND (CTVINC_802=7) AND (C_PARA_802=@D1) and (CODVINC_802 = @D2)", CNN2)
            COMANDO2.Parameters.Add(New SqlParameter("D1", cloca))
            COMANDO2.Parameters.Add(New SqlParameter("D2", cpart))
            COMANDO2.ExecuteNonQuery()
            Dim LECTOR2 As SqlDataReader = COMANDO2.ExecuteReader
            If LECTOR2.Read Then
                resp = LECTOR2.GetValue(0)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            CNN2.Close()
        End Try
        Return resp

    End Function
End Class
