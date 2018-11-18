import React, { Component } from 'react';
import logo from './logo.svg';
import './App.css';
import {uploadImage} from './utils/Api';

class App extends Component {
  constructor(props) {
    super(props);
    this.state = {
      file: '',
      imageSelected: ''
    };
    this._handleImageChange = this._handleImageChange.bind(this);
    this._handleSubmit = this._handleSubmit.bind(this);
  }

  render() {
    let { imageSelected } = this.state;
    let $imagePreviewDiv = null;

    if (imageSelected) {
      $imagePreviewDiv = (<img className="image" src={imageSelected} height="70%" width="70%" />);
    }

    return (
      <div className="App">
        <header className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <div>
            <form onSubmit={this._handleSubmit}>
              <input type="file" onChange={this._handleImageChange} />
              <button type="submit" disabled={!$imagePreviewDiv} onClick={this._handleSubmit}>Crop Border</button>
            </form>
            <div >
              {$imagePreviewDiv}
            </div>
          </div>
        </header>
      </div>

    )
  }

  _handleSubmit(e) {
    e.preventDefault();
    uploadImage(this.state.file);
  }

  _handleImageChange(e) {
    e.preventDefault();

    let reader = new FileReader();
    let file = e.target.files[0];
    reader.onloadend = () => {
      this.setState({
        file: file,
        imageSelected: reader.result
      });
    }
    reader.readAsDataURL(file)
  }

}
export default App;
