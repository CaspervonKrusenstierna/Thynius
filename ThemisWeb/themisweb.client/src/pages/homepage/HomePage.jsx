
import { Header } from "../../shared/components/index"
import MainContentContainer from "../../shared/components/maincontentcontainer/MainContentContainer"
import Footer from "../../shared/components/footer/Footer"
import WaveBackgroundBox from "../../shared/components/wavebackgroundbox/WaveBackgroundBox"
import "./HomePage.css"

const HomePage = () => {
  return (
      <div className="flex-column">
          <WaveBackgroundBox></WaveBackgroundBox>
          <Header></Header>  
          <MainContentContainer></MainContentContainer>
          <Footer></Footer>
      </div>
  )
}

export default HomePage