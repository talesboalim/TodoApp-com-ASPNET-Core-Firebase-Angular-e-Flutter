import 'package:flutter/material.dart';
import 'package:todos/theme/app.theme.dart';
import 'package:todos/views/login.view.dart';

void main() {
  runApp(MyApp());
}

class MyApp extends StatelessWidget {
  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      title: 'Todos',
      theme: appTheme(),
      home: LoginView(),
    );
  }
}

class HomePage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Container(
        child: LoginView(),
      ),
    );
  }
}
