<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PCOMP006
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PCOMP006))
        Me.Label16 = New System.Windows.Forms.Label()
        Me.dgvC = New System.Windows.Forms.DataGridView()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MenuContrato = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.EliminarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AgregarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtOBS = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.ocPrecio = New System.Windows.Forms.CheckBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TxtValor = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TxtCodmat = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.B_Agregar_Item = New System.Windows.Forms.Button()
        Me.B_Entregar = New System.Windows.Forms.Button()
        Me.dgv1 = New System.Windows.Forms.DataGridView()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.EliminarTemSeleccionadoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MODIFICARPRECIOToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MODIFICARCANTIDADToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.CB_Material = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lbNumero = New System.Windows.Forms.Label()
        Me.lbFecha = New System.Windows.Forms.Label()
        Me.lbProveedor = New System.Windows.Forms.Label()
        Me.lbporcentaje = New System.Windows.Forms.Label()
        Me.lbUnidad = New System.Windows.Forms.Label()
        CType(Me.dgvC, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuContrato.SuspendLayout()
        CType(Me.dgv1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!)
        Me.Label16.ForeColor = System.Drawing.Color.Red
        Me.Label16.Location = New System.Drawing.Point(876, 242)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(193, 9)
        Me.Label16.TabIndex = 246
        Me.Label16.Text = "BOTON DERCHO DEL MOUSE PARA ABRIR MENU"
        '
        'dgvC
        '
        Me.dgvC.AllowUserToAddRows = False
        Me.dgvC.AllowUserToDeleteRows = False
        Me.dgvC.BackgroundColor = System.Drawing.SystemColors.Control
        Me.dgvC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvC.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column7, Me.Column8, Me.Column9})
        Me.dgvC.ContextMenuStrip = Me.MenuContrato
        Me.dgvC.Location = New System.Drawing.Point(879, 106)
        Me.dgvC.MultiSelect = False
        Me.dgvC.Name = "dgvC"
        Me.dgvC.ReadOnly = True
        Me.dgvC.RowHeadersVisible = False
        Me.dgvC.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvC.Size = New System.Drawing.Size(284, 131)
        Me.dgvC.TabIndex = 245
        '
        'Column7
        '
        Me.Column7.HeaderText = "cod"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        Me.Column7.Visible = False
        '
        'Column8
        '
        Me.Column8.HeaderText = "Nombre"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        Me.Column8.Width = 200
        '
        'Column9
        '
        Me.Column9.HeaderText = "%"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        Me.Column9.Width = 50
        '
        'MenuContrato
        '
        Me.MenuContrato.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EliminarToolStripMenuItem, Me.AgregarToolStripMenuItem, Me.EditarToolStripMenuItem})
        Me.MenuContrato.Name = "MenuContrato"
        Me.MenuContrato.Size = New System.Drawing.Size(118, 70)
        '
        'EliminarToolStripMenuItem
        '
        Me.EliminarToolStripMenuItem.Name = "EliminarToolStripMenuItem"
        Me.EliminarToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.EliminarToolStripMenuItem.Text = "Eliminar"
        '
        'AgregarToolStripMenuItem
        '
        Me.AgregarToolStripMenuItem.Name = "AgregarToolStripMenuItem"
        Me.AgregarToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.AgregarToolStripMenuItem.Text = "Agregar"
        '
        'EditarToolStripMenuItem
        '
        Me.EditarToolStripMenuItem.Name = "EditarToolStripMenuItem"
        Me.EditarToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.EditarToolStripMenuItem.Text = "Editar"
        '
        'txtTotal
        '
        Me.txtTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotal.Enabled = False
        Me.txtTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotal.Location = New System.Drawing.Point(914, 478)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.Size = New System.Drawing.Size(254, 26)
        Me.txtTotal.TabIndex = 243
        Me.txtTotal.TabStop = False
        Me.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(956, 455)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(176, 20)
        Me.Label14.TabIndex = 244
        Me.Label14.Text = "Valor Orden de Compra"
        '
        'txtOBS
        '
        Me.txtOBS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOBS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtOBS.Enabled = False
        Me.txtOBS.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOBS.Location = New System.Drawing.Point(889, 320)
        Me.txtOBS.Multiline = True
        Me.txtOBS.Name = "txtOBS"
        Me.txtOBS.Size = New System.Drawing.Size(279, 130)
        Me.txtOBS.TabIndex = 225
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(885, 297)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(149, 20)
        Me.Label13.TabIndex = 242
        Me.Label13.Text = "OBSERVACIONES"
        '
        'ocPrecio
        '
        Me.ocPrecio.AutoSize = True
        Me.ocPrecio.Checked = True
        Me.ocPrecio.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ocPrecio.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ocPrecio.Location = New System.Drawing.Point(17, 129)
        Me.ocPrecio.Name = "ocPrecio"
        Me.ocPrecio.Size = New System.Drawing.Size(229, 24)
        Me.ocPrecio.TabIndex = 241
        Me.ocPrecio.Text = "Orden de compra con Precio"
        Me.ocPrecio.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.ForeColor = System.Drawing.Color.Red
        Me.Label10.Location = New System.Drawing.Point(14, 560)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(263, 13)
        Me.Label10.TabIndex = 240
        Me.Label10.Text = "BOTON DERCHO DEL MOUSE PARA ABRIR MENU"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.ForeColor = System.Drawing.Color.Red
        Me.Label12.Location = New System.Drawing.Point(15, 224)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(132, 13)
        Me.Label12.TabIndex = 239
        Me.Label12.Text = "Enter para Buscar por Cod"
        '
        'TxtValor
        '
        Me.TxtValor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtValor.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtValor.Location = New System.Drawing.Point(692, 194)
        Me.TxtValor.Name = "TxtValor"
        Me.TxtValor.Size = New System.Drawing.Size(99, 26)
        Me.TxtValor.TabIndex = 223
        Me.TxtValor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(688, 171)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(46, 20)
        Me.Label11.TabIndex = 238
        Me.Label11.Text = "Valor"
        '
        'TxtCodmat
        '
        Me.TxtCodmat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtCodmat.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCodmat.Location = New System.Drawing.Point(18, 195)
        Me.TxtCodmat.Name = "TxtCodmat"
        Me.TxtCodmat.Size = New System.Drawing.Size(99, 26)
        Me.TxtCodmat.TabIndex = 237
        Me.TxtCodmat.TabStop = False
        Me.TxtCodmat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(875, 83)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(75, 20)
        Me.Label9.TabIndex = 236
        Me.Label9.Text = "Contrato:"
        '
        'B_Agregar_Item
        '
        Me.B_Agregar_Item.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B_Agregar_Item.Location = New System.Drawing.Point(793, 192)
        Me.B_Agregar_Item.Name = "B_Agregar_Item"
        Me.B_Agregar_Item.Size = New System.Drawing.Size(80, 30)
        Me.B_Agregar_Item.TabIndex = 224
        Me.B_Agregar_Item.TabStop = False
        Me.B_Agregar_Item.Text = "Agregar"
        Me.B_Agregar_Item.UseVisualStyleBackColor = True
        '
        'B_Entregar
        '
        Me.B_Entregar.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B_Entregar.Location = New System.Drawing.Point(1055, 523)
        Me.B_Entregar.Name = "B_Entregar"
        Me.B_Entregar.Size = New System.Drawing.Size(113, 35)
        Me.B_Entregar.TabIndex = 226
        Me.B_Entregar.TabStop = False
        Me.B_Entregar.Text = "Guardar"
        Me.B_Entregar.UseVisualStyleBackColor = True
        '
        'dgv1
        '
        Me.dgv1.AllowUserToAddRows = False
        Me.dgv1.AllowUserToDeleteRows = False
        Me.dgv1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight
        Me.dgv1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenHorizontal
        Me.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column3, Me.Column1, Me.Column4, Me.Column2, Me.Column5, Me.Column6})
        Me.dgv1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.dgv1.Location = New System.Drawing.Point(18, 247)
        Me.dgv1.MultiSelect = False
        Me.dgv1.Name = "dgv1"
        Me.dgv1.ReadOnly = True
        Me.dgv1.RowHeadersVisible = False
        Me.dgv1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv1.Size = New System.Drawing.Size(855, 310)
        Me.dgv1.TabIndex = 235
        Me.dgv1.TabStop = False
        '
        'Column3
        '
        Me.Column3.HeaderText = "CODIGO"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        '
        'Column1
        '
        Me.Column1.HeaderText = "DESCRIPCION"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 400
        '
        'Column4
        '
        Me.Column4.HeaderText = "U"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Width = 40
        '
        'Column2
        '
        Me.Column2.HeaderText = "CANTIDAD"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Column5
        '
        Me.Column5.HeaderText = "$/u"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.Width = 80
        '
        'Column6
        '
        Me.Column6.HeaderText = "SUB TOTAL"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EliminarTemSeleccionadoToolStripMenuItem, Me.MODIFICARPRECIOToolStripMenuItem, Me.MODIFICARCANTIDADToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(199, 70)
        '
        'EliminarTemSeleccionadoToolStripMenuItem
        '
        Me.EliminarTemSeleccionadoToolStripMenuItem.Name = "EliminarTemSeleccionadoToolStripMenuItem"
        Me.EliminarTemSeleccionadoToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.EliminarTemSeleccionadoToolStripMenuItem.Text = "ELIMINAR ITEM"
        '
        'MODIFICARPRECIOToolStripMenuItem
        '
        Me.MODIFICARPRECIOToolStripMenuItem.Name = "MODIFICARPRECIOToolStripMenuItem"
        Me.MODIFICARPRECIOToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.MODIFICARPRECIOToolStripMenuItem.Text = "MODIFICAR PRECIO"
        '
        'MODIFICARCANTIDADToolStripMenuItem
        '
        Me.MODIFICARCANTIDADToolStripMenuItem.Name = "MODIFICARCANTIDADToolStripMenuItem"
        Me.MODIFICARCANTIDADToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.MODIFICARCANTIDADToolStripMenuItem.Text = "MODIFICAR CANTIDAD"
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(587, 194)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(99, 26)
        Me.TextBox1.TabIndex = 222
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(583, 171)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(43, 20)
        Me.Label8.TabIndex = 234
        Me.Label8.Text = "Cant"
        '
        'CB_Material
        '
        Me.CB_Material.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CB_Material.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CB_Material.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CB_Material.FormattingEnabled = True
        Me.CB_Material.Location = New System.Drawing.Point(123, 193)
        Me.CB_Material.Name = "CB_Material"
        Me.CB_Material.Size = New System.Drawing.Size(458, 28)
        Me.CB_Material.TabIndex = 221
        Me.CB_Material.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(14, 172)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 20)
        Me.Label2.TabIndex = 233
        Me.Label2.Text = "Material"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(14, 101)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(81, 20)
        Me.Label6.TabIndex = 232
        Me.Label6.Text = "Proveedor"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(307, 29)
        Me.Label1.TabIndex = 228
        Me.Label1.Text = "Modificar Orden de compra"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(904, 29)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 20)
        Me.Label3.TabIndex = 247
        Me.Label3.Text = "Nunero"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(16, 60)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(54, 20)
        Me.Label4.TabIndex = 248
        Me.Label4.Text = "Fecha"
        '
        'lbNumero
        '
        Me.lbNumero.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbNumero.Location = New System.Drawing.Point(971, 21)
        Me.lbNumero.Name = "lbNumero"
        Me.lbNumero.Size = New System.Drawing.Size(179, 32)
        Me.lbNumero.TabIndex = 249
        Me.lbNumero.Text = "Nunero"
        Me.lbNumero.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbFecha
        '
        Me.lbFecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbFecha.Location = New System.Drawing.Point(76, 60)
        Me.lbFecha.Name = "lbFecha"
        Me.lbFecha.Size = New System.Drawing.Size(382, 20)
        Me.lbFecha.TabIndex = 250
        Me.lbFecha.Text = "Nunero"
        '
        'lbProveedor
        '
        Me.lbProveedor.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbProveedor.Location = New System.Drawing.Point(119, 101)
        Me.lbProveedor.Name = "lbProveedor"
        Me.lbProveedor.Size = New System.Drawing.Size(382, 20)
        Me.lbProveedor.TabIndex = 251
        Me.lbProveedor.Text = "Nunero"
        '
        'lbporcentaje
        '
        Me.lbporcentaje.Location = New System.Drawing.Point(1075, 242)
        Me.lbporcentaje.Name = "lbporcentaje"
        Me.lbporcentaje.Size = New System.Drawing.Size(88, 26)
        Me.lbporcentaje.TabIndex = 252
        Me.lbporcentaje.Text = "0"
        Me.lbporcentaje.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbUnidad
        '
        Me.lbUnidad.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbUnidad.Location = New System.Drawing.Point(636, 172)
        Me.lbUnidad.Name = "lbUnidad"
        Me.lbUnidad.Size = New System.Drawing.Size(50, 20)
        Me.lbUnidad.TabIndex = 253
        Me.lbUnidad.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'PCOMP006
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1192, 614)
        Me.Controls.Add(Me.lbUnidad)
        Me.Controls.Add(Me.lbporcentaje)
        Me.Controls.Add(Me.lbProveedor)
        Me.Controls.Add(Me.lbFecha)
        Me.Controls.Add(Me.lbNumero)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.dgvC)
        Me.Controls.Add(Me.txtTotal)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txtOBS)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.ocPrecio)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.TxtValor)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.TxtCodmat)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.B_Agregar_Item)
        Me.Controls.Add(Me.B_Entregar)
        Me.Controls.Add(Me.dgv1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.CB_Material)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "PCOMP006"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PCOMP006"
        CType(Me.dgvC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuContrato.ResumeLayout(False)
        CType(Me.dgv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label16 As Label
    Friend WithEvents dgvC As DataGridView
    Friend WithEvents Column7 As DataGridViewTextBoxColumn
    Friend WithEvents Column8 As DataGridViewTextBoxColumn
    Friend WithEvents Column9 As DataGridViewTextBoxColumn
    Friend WithEvents txtTotal As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents txtOBS As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents ocPrecio As CheckBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents TxtValor As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents TxtCodmat As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents B_Agregar_Item As Button
    Friend WithEvents B_Entregar As Button
    Friend WithEvents dgv1 As DataGridView
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents CB_Material As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents lbNumero As Label
    Friend WithEvents lbFecha As Label
    Friend WithEvents lbProveedor As Label
    Friend WithEvents MenuContrato As ContextMenuStrip
    Friend WithEvents EliminarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AgregarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EditarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents EliminarTemSeleccionadoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MODIFICARPRECIOToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MODIFICARCANTIDADToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents lbporcentaje As Label
    Friend WithEvents lbUnidad As Label
End Class
