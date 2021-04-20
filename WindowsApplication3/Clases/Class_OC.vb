Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.IO
Imports CrystalDecisions.Shared

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
    Public Sub grabar_cab_OC(ByVal _NOC As Decimal, ByVal FALTA As Date, ByVal USER As String, ByVal PROV As String, ByVal TIPO As Integer, ByVal CONTRATO As String, tienePrecio As Integer, Observaciones As String, precioFinal As Decimal)
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("insert T_C_OC_105 (N_OC_105,F_ALTA_105,USERG_105,ESTA_105,C_PROV_105,TIPO_OC_105, CONT_105, OBS_105,MONTO_105,CONPRECIO_105) values(@D1,@D2,@D3,@D4,@D5,@D6,@D10,@D11,@D12,@D13)", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", _NOC))
        comando1.Parameters.Add(New SqlParameter("D2", FALTA))
        comando1.Parameters.Add(New SqlParameter("D3", USER))
        comando1.Parameters.Add(New SqlParameter("D4", 1))
        comando1.Parameters.Add(New SqlParameter("D5", PROV))
        comando1.Parameters.Add(New SqlParameter("D6", TIPO))
        comando1.Parameters.Add(New SqlParameter("D10", CONTRATO))
        comando1.Parameters.Add(New SqlParameter("D11", Observaciones))
        comando1.Parameters.Add(New SqlParameter("D12", precioFinal))
        comando1.Parameters.Add(New SqlParameter("D13", tienePrecio))




        comando1.ExecuteNonQuery()
        con1.Close()
    End Sub

    Public Sub Edicar_cab_OC(ByVal _NOC As Decimal, tienePrecio As Integer, Observaciones As String, precioFinal As Decimal)
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        Try
            con1.Open()
            'creo el comando para pasarle los parametros
            Dim comando1 As New SqlClient.SqlCommand("UPDATE T_C_OC_105 SET OBS_105=@D1, MONTO_105=@D2, CONPRECIO_105=@D3 WHERE N_OC_105=@E1", con1)
            'creo el lector de parametros
            comando1.Parameters.Add(New SqlParameter("D1", Observaciones))
            comando1.Parameters.Add(New SqlParameter("D2", precioFinal))
            comando1.Parameters.Add(New SqlParameter("D3", tienePrecio))
            comando1.Parameters.Add(New SqlParameter("E1", _NOC))
            comando1.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con1.Close()
        End Try


    End Sub
    Public Sub grabar_det_oc(ByVal NOC As Decimal, ByVal MATERIAL As String, ByVal CANTS As Decimal, ByVal CANTE As Decimal, ByVal CONF As Boolean, precio As Decimal)
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("insert T_D_OC_106 (N_OC_106, C_MATE_106, CANT_106, CANTE_106,CONF_106,PRECIO_C_106) values(@D1,@D2,@D3,@D4,@D5,@D6)", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", NOC))
        comando1.Parameters.Add(New SqlParameter("D2", MATERIAL))
        comando1.Parameters.Add(New SqlParameter("D3", CANTS))
        comando1.Parameters.Add(New SqlParameter("D4", CANTE))
        comando1.Parameters.Add(New SqlParameter("D5", CONF))
        comando1.Parameters.Add(New SqlParameter("D6", precio))
        comando1.ExecuteNonQuery()
        con1.Close()
    End Sub

    Public Sub grabar_contrato(ggv As DataGridView, noc As Decimal)
        Dim con As SqlConnection = New SqlConnection(conexion)
        For i = 0 To ggv.Rows.Count - 1
            Try
                con.Open()
                Dim adt As New SqlCommand("INSERT INTO T_C_OC_105C (NOC_105C,CONT_105C,CANT_105C) VALUES (@D1,@D2,@D3)", con)
                adt.Parameters.AddWithValue("D1", noc)
                adt.Parameters.AddWithValue("D2", ggv.Item(0, i).Value)
                adt.Parameters.AddWithValue("D3", CDec(ggv.Item(2, i).Value))
                adt.ExecuteNonQuery()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
                con.Close()
            End Try
        Next
    End Sub

    Public Sub guardarMovimiento(noc As Decimal, fechamov As DateTime, tipomov As Integer, user As String)
        Dim con As SqlConnection = New SqlConnection(conexion)

        Try
                con.Open()
            Dim adt As New SqlCommand("INSERT INTO T_MOV_OC_125 (NOC_125,FECH_MOV_125,TIPO_MOV_125, USER_125) VALUES (@D1,@D2,@D3,@D4)", con)
            adt.Parameters.AddWithValue("D1", noc)
            adt.Parameters.AddWithValue("D2", fechamov)
            adt.Parameters.AddWithValue("D3", tipomov)
            adt.Parameters.AddWithValue("D4", user)
            adt.ExecuteNonQuery()
        Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
                con.Close()
            End Try

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

    Public Sub GrabarUltValor(ByVal proov As String, ByVal valor As Integer)
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("INSERT INTO M_ULT_VALOR_010 (PROVE_010, VALOR_010, FECHA_010) VALUES (@D1,@D2,@D3)", con1)
        comando1.Parameters.Add(New SqlParameter("D1", proov))
        comando1.Parameters.Add(New SqlParameter("D2", valor))
        comando1.Parameters.Add(New SqlParameter("D3", DateTime.Now))
        'Execute
        comando1.ExecuteNonQuery()
        con1.Close()
    End Sub

    Public Sub ImprimirOC(ncompra As Decimal)
        Dim cnn As New SqlConnection(conexion)
        Dim Ds As New DS_OC
        Dim fecha As DateTime = Date.Now
        Dim cuit As String = ""
        Dim razon As String = ""
        Dim direccion As String = ""
        Dim contrato As String = ""
        Dim tienevalor As Integer = 0
        Dim obs As String = ""
        Dim confecciono As String = ""
        Dim aprobo As String = ""
        Dim valor As Decimal = 0
        Dim N_REMITO As String = ""
        Try
            cnn.Open()
            Dim adt As New SqlCommand("SELECT T_C_OC_105.N_OC_105, T_C_OC_105.F_ALTA_105, T_C_OC_105.C_PROV_105, M_PROV_005.RAZO_005, M_PROV_005.DIRE_005 + ' - ' + DET_PARAMETRO_802.DESC_802 AS DIRECCION, T_C_OC_105.CONT_105  AS CONTRATO, T_C_OC_105.OBS_105, T_C_OC_105.MONTO_105, T_C_OC_105.CONPRECIO_105, APROBO.APELL_001 + ' ' + APROBO.NOMB_001 AS APROBO, CONFECCIONO.APELL_001 + ' ' + CONFECCIONO.NOMB_001 AS CONFECCIONO FROM M_USRS_001 AS CONFECCIONO INNER JOIN T_C_OC_105 ON CONFECCIONO.NDOC_001 = T_C_OC_105.USERG_105 INNER JOIN M_PROV_005 ON T_C_OC_105.C_PROV_105 = M_PROV_005.CUIT_005 INNER JOIN DET_PARAMETRO_802 ON M_PROV_005.LOCA_005 = DET_PARAMETRO_802.C_PARA_802 INNER JOIN M_USRS_001 AS APROBO ON T_C_OC_105.USERR_105 = APROBO.NDOC_001 WHERE (DET_PARAMETRO_802.C_TABLA_802 = 8) AND (T_C_OC_105.N_OC_105 =@D1)", cnn)
            adt.Parameters.AddWithValue("D1", ncompra)
            Dim lector As SqlDataReader = adt.ExecuteReader
            Do While lector.Read
                fecha = lector.GetDateTime(1)
                cuit = lector.GetString(2)
                razon = lector.GetString(3) + "(" + cuit + ")"
                direccion = lector.GetString(4)
                contrato = lector.GetString(5)
                If IsDBNull(lector.GetValue(6)) = False Then
                    obs = lector.GetString(6)
                End If
                If IsDBNull(lector.GetValue(7)) = False Then
                    valor = lector.GetValue(7)
                End If
                If IsDBNull(lector.GetValue(8)) = False Then
                    tienevalor = lector.GetValue(8)
                End If
                aprobo = lector.GetString(9)
                confecciono = lector.GetString(10)
                If _usr.Obt_Almacen = 0 Then
                    N_REMITO = "0100" + "-" + ncompra.ToString.PadLeft(8, "0")
                Else
                    N_REMITO = _usr.Obt_Almacen.ToString.PadLeft(4, "0") + "-" + ncompra.ToString.PadLeft(8, "0")
                End If

                Ds.CABEZERA.Rows.Add(N_REMITO, fecha, cuit, razon, contrato, tienevalor.ToString, FormatNumber(valor, 2).ToString, direccion, obs, confecciono, aprobo)



            Loop
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        'SELECT T_D_OC_106.N_OC_106, T_D_OC_106.C_MATE_106, M_MATE_002.DESC_002, M_MATE_002.UNID_002, T_D_OC_106.CANT_106, T_D_OC_106.PRECIO_C_106 FROM T_D_OC_106 INNER JOIN M_MATE_002 ON T_D_OC_106.C_MATE_106 = M_MATE_002.CMATE_002 WHERE (T_D_OC_106.N_OC_106 = @D1)
        Try
            cnn.Open()
            Dim adt2 As New SqlCommand("SELECT T_D_OC_106.N_OC_106, T_D_OC_106.C_MATE_106, M_MATE_002.DESC_002, M_MATE_002.UNID_002, T_D_OC_106.CANT_106, T_D_OC_106.PRECIO_C_106 FROM T_D_OC_106 INNER JOIN M_MATE_002 ON T_D_OC_106.C_MATE_106 = M_MATE_002.CMATE_002 WHERE (T_D_OC_106.N_OC_106 = @D1)", cnn)
            adt2.Parameters.AddWithValue("D1", ncompra)
            Dim lector2 As SqlDataReader = adt2.ExecuteReader
            Do While lector2.Read
                Dim CANTIDAD As Decimal = 0
                Dim PRECIOU As Decimal = 0
                Dim TOTAl_ITEM As Decimal = 0
                If IsDBNull(lector2.GetValue(4)) = False Then
                    CANTIDAD = FormatNumber(lector2.GetValue(4), 2)
                End If
                If IsDBNull(lector2.GetValue(5)) = False Then
                    PRECIOU = FormatNumber(lector2.GetValue(5), 2)
                End If
                TOTAl_ITEM = PRECIOU * CANTIDAD

                Ds.DETALLE.Rows.Add(N_REMITO, lector2.GetString(1), lector2.GetString(2), lector2.GetString(3), FormatNumber(CANTIDAD, 2).ToString, FormatNumber(PRECIOU, 2).ToString, FormatNumber(TOTAl_ITEM, 2).ToString)

            Loop
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        Try
            cnn.Open()
            Dim adt2 As New SqlCommand("SELECT T_C_OC_105C.NOC_105C, T_C_OC_105C.CONT_105C, M_CONT_004.DESC_004, T_C_OC_105C.CANT_105C FROM T_C_OC_105C INNER JOIN M_CONT_004 ON T_C_OC_105C.CONT_105C = M_CONT_004.NCONT_004 WHERE (T_C_OC_105C.NOC_105C = @D1) ORDER BY M_CONT_004.DESC_004 ", cnn)
            adt2.Parameters.AddWithValue("D1", ncompra)
            Dim lector2 As SqlDataReader = adt2.ExecuteReader
            Do While lector2.Read
                Ds.CONTRATO.Rows.Add(lector2.GetValue(0).ToString, lector2.GetValue(1).ToString, lector2.GetValue(1).ToString + "-" + lector2.GetValue(2), lector2.GetValue(3).ToString)
            Loop
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        Dim ARCHIVO As String = "C:\ARCHIVO\" + N_REMITO.ToString + "-" + Date.Now.ToString("dd-MM-yyyy-HHmm") + ".pdf"
        If tienevalor = 0 Then
            Dim oc As New OrdenDeCompraSinValor
            oc.SetDataSource(Ds)
            oc.ExportToDisk(ExportFormatType.PortableDocFormat, ARCHIVO)
            Process.Start(ARCHIVO)
        Else
            Dim oc As New OrdenDeCompra
            oc.SetDataSource(Ds)
            oc.ExportToDisk(ExportFormatType.PortableDocFormat, ARCHIVO)
            Process.Start(ARCHIVO)
        End If


    End Sub
    Public Sub ImprimirDetalleOC(ncompra As Decimal)
        Dim cnn As New SqlConnection(conexion)
        Dim Ds As New DS_OC
        Dim fecha As DateTime = Date.Now
        Dim cuit As String = ""
        Dim razon As String = ""
        Dim direccion As String = ""
        Dim contrato As String = ""
        Dim tienevalor As Integer = 0
        Dim CANTE As Decimal = 0
        Dim SALDO As Decimal = 0
        Dim obs As String = ""
        Dim confecciono As String = ""
        Dim aprobo As String = ""
        Dim valor As Decimal = 0
        Dim N_REMITO As String = ""
        Try
            cnn.Open()
            Dim adt As New SqlCommand("SELECT T_C_OC_105.N_OC_105, T_C_OC_105.F_ALTA_105, T_C_OC_105.C_PROV_105, M_PROV_005.RAZO_005, M_PROV_005.DIRE_005 + ' - ' + DET_PARAMETRO_802.DESC_802 AS DIRECCION, T_C_OC_105.CONT_105  AS CONTRATO, T_C_OC_105.OBS_105, T_C_OC_105.MONTO_105, T_C_OC_105.CONPRECIO_105, APROBO.APELL_001 + ' ' + APROBO.NOMB_001 AS APROBO, CONFECCIONO.APELL_001 + ' ' + CONFECCIONO.NOMB_001 AS CONFECCIONO FROM M_USRS_001 AS CONFECCIONO INNER JOIN T_C_OC_105 ON CONFECCIONO.NDOC_001 = T_C_OC_105.USERG_105 INNER JOIN M_PROV_005 ON T_C_OC_105.C_PROV_105 = M_PROV_005.CUIT_005 INNER JOIN DET_PARAMETRO_802 ON M_PROV_005.LOCA_005 = DET_PARAMETRO_802.C_PARA_802 INNER JOIN M_USRS_001 AS APROBO ON T_C_OC_105.USERR_105 = APROBO.NDOC_001 WHERE (DET_PARAMETRO_802.C_TABLA_802 = 8) AND (T_C_OC_105.N_OC_105 =@D1)", cnn)
            adt.Parameters.AddWithValue("D1", ncompra)
            Dim lector As SqlDataReader = adt.ExecuteReader
            Do While lector.Read
                fecha = lector.GetDateTime(1)
                cuit = lector.GetString(2)
                razon = lector.GetString(3) + "(" + cuit + ")"
                direccion = lector.GetString(4)
                contrato = lector.GetString(5)
                If IsDBNull(lector.GetValue(6)) = False Then
                    obs = lector.GetString(6)
                End If
                If IsDBNull(lector.GetValue(7)) = False Then
                    valor = lector.GetValue(7)
                End If
                If IsDBNull(lector.GetValue(8)) = False Then
                    tienevalor = lector.GetValue(8)
                End If
                aprobo = lector.GetString(9)
                confecciono = lector.GetString(10)
                If _usr.Obt_Almacen = 0 Then
                    N_REMITO = "0100" + "-" + ncompra.ToString.PadLeft(8, "0")
                Else
                    N_REMITO = _usr.Obt_Almacen.ToString.PadLeft(4, "0") + "-" + ncompra.ToString.PadLeft(8, "0")
                End If

                Ds.CABEZERA.Rows.Add(N_REMITO, fecha, cuit, razon, contrato, tienevalor.ToString, FormatNumber(valor, 2).ToString, direccion, obs, confecciono, aprobo)



            Loop
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        'SELECT T_D_OC_106.N_OC_106, T_D_OC_106.C_MATE_106, M_MATE_002.DESC_002, M_MATE_002.UNID_002, T_D_OC_106.CANT_106, T_D_OC_106.PRECIO_C_106 FROM T_D_OC_106 INNER JOIN M_MATE_002 ON T_D_OC_106.C_MATE_106 = M_MATE_002.CMATE_002 WHERE (T_D_OC_106.N_OC_106 = @D1)
        Try
            cnn.Open()
            Dim adt2 As New SqlCommand("SELECT T_D_OC_106.N_OC_106, T_D_OC_106.C_MATE_106, M_MATE_002.DESC_002, M_MATE_002.UNID_002, T_D_OC_106.CANT_106, T_D_OC_106.PRECIO_C_106,T_D_OC_106.CANTE_106 FROM T_D_OC_106 INNER JOIN M_MATE_002 ON T_D_OC_106.C_MATE_106 = M_MATE_002.CMATE_002 WHERE (T_D_OC_106.N_OC_106 = @D1) ", cnn)
            adt2.Parameters.AddWithValue("D1", ncompra)
            Dim lector2 As SqlDataReader = adt2.ExecuteReader
            Do While lector2.Read
                Dim CANTIDAD As Decimal = 0
                Dim PRECIOU As Decimal = 0
                Dim TOTAl_ITEM As Decimal = 0
                If IsDBNull(lector2.GetValue(4)) = False Then
                    CANTIDAD = FormatNumber(lector2.GetValue(4), 2)
                End If
                If IsDBNull(lector2.GetValue(5)) = False Then
                    PRECIOU = FormatNumber(lector2.GetValue(5), 2)
                End If
                TOTAl_ITEM = PRECIOU * CANTIDAD
                If IsDBNull(lector2.GetValue(6)) Then
                    CANTE = 0

                Else
                    CANTE = FormatNumber(lector2.GetValue(6), 2)
                End If
                SALDO = CANTIDAD - CANTE

                Ds.DETALLE.Rows.Add(N_REMITO, lector2.GetString(1), lector2.GetString(2), lector2.GetString(3), FormatNumber(CANTIDAD, 2).ToString, FormatNumber(PRECIOU, 2).ToString, FormatNumber(TOTAl_ITEM, 2).ToString, CANTE, SALDO)

            Loop
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        Try
            cnn.Open()
            Dim adt2 As New SqlCommand("SELECT T_C_OC_105C.NOC_105C, T_C_OC_105C.CONT_105C, M_CONT_004.DESC_004, T_C_OC_105C.CANT_105C FROM T_C_OC_105C INNER JOIN M_CONT_004 ON T_C_OC_105C.CONT_105C = M_CONT_004.NCONT_004 WHERE (T_C_OC_105C.NOC_105C = @D1) ORDER BY M_CONT_004.DESC_004 ", cnn)
            adt2.Parameters.AddWithValue("D1", ncompra)
            Dim lector2 As SqlDataReader = adt2.ExecuteReader
            Do While lector2.Read
                Ds.CONTRATO.Rows.Add(lector2.GetValue(0).ToString, lector2.GetValue(1).ToString, lector2.GetValue(1).ToString + "-" + lector2.GetValue(2), lector2.GetValue(3).ToString)
            Loop
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        Dim ARCHIVO As String = "C:\ARCHIVO\Detalle_" + N_REMITO.ToString + "-" + Date.Now.ToString("dd-MM-yyyy-HHmm") + ".pdf"

        Dim oc As New ReporteOrdenCompra
        oc.SetDataSource(Ds)
            oc.ExportToDisk(ExportFormatType.PortableDocFormat, ARCHIVO)
            Process.Start(ARCHIVO)



    End Sub
    Public Sub EliminarContratoyDetalle(noc As Decimal)
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("DELETE FROM T_D_OC_106 WHERE N_OC_106=@D1", cnn)
            adt.Parameters.AddWithValue("D1", noc)
            adt.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        Try
            cnn.Open()
            Dim adt As New SqlCommand("DELETE FROM T_C_OC_105C WHERE NOC_105C=@D1", cnn)
            adt.Parameters.AddWithValue("D1", noc)
            adt.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()

        End Try

    End Sub

End Class
