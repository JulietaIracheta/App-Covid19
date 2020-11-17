USE [TP-20201C]
GO
/*Para probar las funcionalidades del administrador:
-Realizar el siguiente INSERT
-Loguearse con estos datos:
E-mail: admin80@hotmail.com
Contraseña: Cande123*/
INSERT INTO [dbo].[Usuarios]
           ([Nombre]
           ,[Apellido]
           ,[FechaNacimiento]
           ,[UserName]
           ,[Email]
           ,[Password]
           ,[Foto]
           ,[TipoUsuario]
           ,[FechaCreacion]
           ,[Activo]
           ,[Token])
     VALUES
           ('Admin',	
			'Istrador',
			'1980-07-08 00:00:00.000',	
			'Administrador80',	
			'admin80@hotmail.com',	
			'3a74401153b01c0cafac7e33b2137cca38171653',	
			'/Imagenes/2xfg.PNG',	
			2,	
			'2020-06-23 14:35:04.813',	
			1,	
			'XJ-8-&khYE2vqDrMN&6lxN')
GO


