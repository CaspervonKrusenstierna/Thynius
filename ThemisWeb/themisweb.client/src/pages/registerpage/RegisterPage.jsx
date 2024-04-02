import { useEffect } from "react";
import RegisterBox from "./components/registerbox/RegisterBox";
import { DarknessEffect, Header, MainContentContainer, WaveBackgroundBox } from "../../shared/components/homepage";

function RegisterPage() {
    useEffect(() => {
        window.scrollTo(0,0);
    }, [])
    return (
        <>
         <WaveBackgroundBox></WaveBackgroundBox>
         <RegisterBox></RegisterBox>
         <div className='HomePage-unfocus'><DarknessEffect></DarknessEffect><Header></Header><MainContentContainer></MainContentContainer></div>
        </>
    );
}

export default RegisterPage;