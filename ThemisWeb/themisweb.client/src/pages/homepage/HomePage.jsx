
import "./HomePage.css"
import { WaveBackgroundBox, Header, MainContentContainer, Footer } from "../../shared/components/homepage"

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