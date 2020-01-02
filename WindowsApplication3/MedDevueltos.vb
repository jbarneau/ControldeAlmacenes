Public Class MedDevueltos

    Private almacen As String
    Public Property GetSetAlmacen() As String
        Get
            Return almacen
        End Get
        Set(ByVal value As String)
            almacen = value
        End Set
    End Property

    Private operario As String
    Public Property GetSetOperario() As String
        Get
            Return operario
        End Get
        Set(ByVal value As String)
            operario = value
        End Set
    End Property

    Private cantidad As String
    Public Property GetSetCantidad() As String
        Get
            Return cantidad
        End Get
        Set(ByVal value As String)
            cantidad = value
        End Set
    End Property

End Class
