import 'package:firebase_auth/firebase_auth.dart';
import 'package:google_sign_in/google_sign_in.dart';
import 'package:todos/user.dart';

class LoginController {
  final GoogleSignIn _googleSignIn = GoogleSignIn();
  Future login() async {
    final GoogleSignInAccount googleUser = await _googleSignIn.signIn();
    final GoogleSignInAuthentication googleAuth =
        await googleUser.authentication;

    final GoogleAuthCredential googleCredential = GoogleAuthProvider.credential(
      accessToken: googleAuth.accessToken,
      idToken: googleAuth.idToken,
    );

    final UserCredential googleUserCredential =
        await FirebaseAuth.instance.signInWithCredential(googleCredential);

    var token = await googleUserCredential.user.getIdToken();

    user.name = googleUserCredential.user.displayName;
    user.email = googleUserCredential.user.email;
    user.picture = googleUserCredential.user.photoURL;
    user.token = token.toString();
  }

  Future logout() async {
    await FirebaseAuth.instance.signOut();
    user = new IUser();
  }
}
