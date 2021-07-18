import React from 'react';
import ReactDOM from 'react-dom';

class ProductList extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      products: []
    }

    this.handleProductUpVote = this.handleProductUpVote.bind(this);
  }

  componentDidMount() {
    this.setState({ products: window.Seed.products });
  }

  handleProductUpVote(productId) {
    const nextProducts = this.state.products.map((product) => {
      if (product.id === productId) {
        return Object.assign({}, product, {
          votes: product.votes + 1,
        });
      } else {
        return product;
      }
    });
    this.setState({
      products : nextProducts
    });
  }

  render() {
      const products = this.state.products.sort((a, b) => b.votes - a.votes).map((product, i) =>
        <ProductItem
          id={product.id}
          title={product.title}
          description={product.description}
          url={product.url}
          votes={product.votes}
          submitterAvatarUrl={product.submitterAvatarUrl}
          productImageUrl={product.productImageUrl}
          onVote={this.handleProductUpVote} />
      );

      return(
      <div class= "productListOuterContainer" >
        <div>
          <div class="productListHeader">
            <h1>Product List</h1>
          </div>

          {products}

        </div>
      </div>
    );
  }
}

class ProductItem extends React.Component {
  constructor(props) {
    super(props);
    this.upVoteOnClick = this.upVoteOnClick.bind(this);
  }
  
  upVoteOnClick  = () => this.props.onVote(this.props.id);

  render() {
    return (
      <div class="productItemContainer">
        <div>
          <img src={this.props.productImageUrl} class="productItemPhoto" alt={this.props.title} />
        </div>
        <div class="productDetailsContainer">
          <p>
            <a href="#" alt="vote" onClick={this.upVoteOnClick}>
              <i class="fa fa-arrow-up"></i>
            </a>
            <span class="productVotes">{this.props.votes}</span>
          </p>
          <p>
            <a href={this.props.url} class="productName">
              {this.props.title}
            </a>
          </p>
          <p class="productDescription">
            {this.props.description}
          </p>
          <p style={{ color: 'gray' }}>
            Submitted by <img src={this.props.submitterAvatarUrl} class="productItemOwnerImage" alt="" />
          </p>
        </div>
      </div>
    );
  }
}

ReactDOM.render(
  <React.StrictMode>
    {<ProductList />}
  </React.StrictMode>,
  document.getElementById('root')
);
