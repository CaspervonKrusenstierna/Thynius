import { useEffect } from "react";
import LoginBox from "./components/loginbox/LoginBox";
import { DarknessEffect, Header, MainContentContainer, WaveBackgroundBox } from "../../shared/components/homepage";
function LoginPage() {
    useEffect(() => {
        window.scrollTo(0,0);
    }, [])
    return (
        <>
            <WaveBackgroundBox></WaveBackgroundBox>
            <LoginBox></LoginBox>
            <div className='HomePage-unfocus'><DarknessEffect></DarknessEffect><Header></Header><MainContentContainer></MainContentContainer></div>
        </>
  );
}

export default LoginPage;