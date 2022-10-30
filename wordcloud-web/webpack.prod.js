const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const pkgVersion = require('./package.json').version;
const path = require('path');
const { merge } = require('webpack-merge');
const common = require('./webpack.common');
require('dotenv').config({ path: './.env' });

module.exports = merge(common, {
	mode: 'production',
	plugins: [
		new CleanWebpackPlugin(),
		new HtmlWebpackPlugin({
			template: path.join(__dirname, 'src', 'index.html'),
			pkgVersion,
			filename: 'index.html',
			favicon: './src/assets/favicon.ico'
		}),
		new webpack.DefinePlugin({
			'process.env': JSON.stringify(process.env)
		})		
	],
	entry: path.join(__dirname, 'src', 'index.tsx'),
	output: {
		filename: '[name].bundle.js',
		chunkFilename: '[name].[chunkhash].bundle.js',
		publicPath: '/',
		path: path.resolve(__dirname, 'dist'),
		clean: true
	}
})