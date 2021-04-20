Public Class Clase_mensaje

    Public dniope As String
    Public nomope As String
    Public material As Decimal
    Public descmate As String
    Public stockmax As Decimal
    Public apedir As Decimal
    Public stockope As String


    '****************ERRORES*****************ERRORES**********************ERRORES*************************************ERRORES*************************

    Public Sub MERRO001()
        MessageBox.Show("Error en conexión con la base de datos", "MERRO001", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Public Sub MERRO002()
        MessageBox.Show("Debe ingresar un usuario", "MERRO002", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Public Sub MERRO003()
        MessageBox.Show("Ha superado el máximo de intentos" + vbCrLf + "El sistema se cerrara", "MERRO003", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Public Sub MERRO004()
        MessageBox.Show("El usuario no existe", "MERRO004", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Public Sub MERRO005()
        MessageBox.Show("Contraseña incorrecta", "MERRO005", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Public Sub MERRO006()
        MessageBox.Show("Verifique los datos", "MERRO006", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Public Sub MERRO007()
        MessageBox.Show("La cantidad ingresada es mayor al disponible", "MERRO007", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Public Sub MERRO008()
        MessageBox.Show("No se permiten campos vacios" + vbCrLf + "Verifique porfavor", "MERRO008", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Public Sub MERRO009()
        MessageBox.Show("El material no permite decimales", "MERRO009", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Public Sub MERRO010()
        MessageBox.Show("La cantidad es menor a 0" + vbCrLf + "Verifique porfavor", "MERRO010", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Public Sub MERRO011()
        MessageBox.Show("No se han encontrados datos", "MERRO011", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Public Sub MERRO012()
        MessageBox.Show("El equipo posee medidores sin asignar", "MERRO012", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Public Sub MERRO013()
        MessageBox.Show("Solo se permite el ingreso de números enteros", "MERRO0013", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Public Sub MERRO014()
        MessageBox.Show("Debe ingresar 8 digitos", "MERRO014", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Public Sub MERRO015()
        MessageBox.Show("Medidores inexistentes", "MERRO015", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Public Sub MERRO016()
        MessageBox.Show("Debe seleccionar un archivo", "MERRO016", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Public Sub MERRO017()
        MessageBox.Show("El número de peticion ya existe", "MERRO017", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Public Sub MERRO018()
        MessageBox.Show("Debe ingresar almenos un material", "MERRO018", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Public Sub MERRO019()
        MessageBox.Show("Debe seleccionar almenos un item", "MERRO019", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Public Sub MERRO020()
        MessageBox.Show("Debe ingresar 11 digitos sin guiones", "MERRO020", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Public Sub MERRO021()
        MessageBox.Show("Debe ingresar la misma contraseña en los dos recuadros", "MERRO021", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Public Sub MERRO022()
        MessageBox.Show("El Contrato tiene un material pendiente de entrega en una peticiòn", "MERRO022", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Public Sub MERRO023()
        MessageBox.Show("El Nombre y Apellido Ingresado ya existe", "MERRO023", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Public Sub MERRO024()
        MessageBox.Show("El usuario Ingresado ya existe", "MERRO024", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Public Sub MERRO025(ByVal dni As String)
        MessageBox.Show("El operario Ingresado ya existe" + vbCrLf + "Con DNI Nº: " + dni.ToString, "MERRO025", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Public Sub MERRO026()
        MessageBox.Show("Debe ingresar un cuit sin guiones", "MERRO026", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Public Sub MERRO027()
        MessageBox.Show("El deposito ya exista cambie el nombre", "MERRO027", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Public Sub MERRO028()
        MessageBox.Show("El partido ya existe", "MERRO028", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Public Sub MERRO029()
        MessageBox.Show("No se registraron ingresos para esta OC", "MERRO029", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Public Sub MERRO030()
        MessageBox.Show("Debe seleccionar almenos un deposito", "MERRO030", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    Public Sub MERRO031(nmed As String)
        MessageBox.Show("El medidor: " + nmed + " no existe en el recuadro", "MERRO031", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
    '******************CONFIRMACION*******************CONFIRMACION****************************CONFIRMACION***************
    Public Sub MADVE001()

        MessageBox.Show("La operación se realizo con éxito", "MADVE001", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        
    End Sub
    Public Sub MADVE002(ByVal fichero As String)
        Dim resp As DialogResult
        resp = MessageBox.Show("La operación se realizo con éxito" + vbCrLf + "El archivo se encuentra en " + fichero.ToString + vbCrLf + "¿Desea abrirlo?", "MADVE002", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk)
        If resp = DialogResult.Yes Then
            Process.Start(fichero)
        End If
    End Sub
    Public Sub MADVE003()
        MessageBox.Show("La modificación se realizo con éxito", "MADVE003", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
    End Sub
    Public Sub MADVE004(ByVal REMITO As String)
        MessageBox.Show("La operación se realizo con éxito" + vbCrLf + "Se genero el remito Nº: " + REMITO.PadLeft(8, "0"), "MADVE004", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
    End Sub
    Public Sub MADVE005(ByVal cantidad As String, ByVal tipo As String)
        MessageBox.Show("La operación se realizo con éxito" + vbCrLf + "Se cargaron " + cantidad + " " + tipo, "MADVE005", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
    End Sub
    Public Sub MADVE006(ByVal cantidad As String)
        MessageBox.Show("El equipo llego al maximo de " + cantidad + vbCrLf + "Recuerde verificar y seguir el procedimiento antes de entregar", "MADVE006", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
    End Sub

    Public Sub MADVE007(ByVal fichero As String)
        MessageBox.Show("La operación se realizo con éxito" + vbCrLf + "El archivo se encuentra en " + fichero.ToString + vbCrLf + "OK", "MADVE007", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
    End Sub
End Class
