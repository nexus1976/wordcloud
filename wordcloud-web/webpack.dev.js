const path = require('path');
const { merge } = require('webpack-merge');
const common = require('./webpack.common');
const pkgVersion = require('./package.json').version;
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const webpack = require('webpack');
require('dotenv').config({ path: './.env' });

module.exports = merge(common, {
	mode: 'development',
	devtool: 'eval-source-map',
	entry: path.join(__dirname, 'src', 'index.tsx'),
	plugins: [
		new CleanWebpackPlugin(),
		new HtmlWebpackPlugin({
			template: path.join(__dirname, 'src', 'index.html'),
			pkgVersion,
			filename: 'index.html',
			favicon: './src/assets/favicon.ico'
		}),
		new webpack.optimize.LimitChunkCountPlugin({
		  maxChunks: 1 // disable creating additional chunks
		}),
		new webpack.DefinePlugin({
			'process.env': JSON.stringify(process.env)
		})
	],
	output: {
		path: path.join(__dirname, 'dist'),
		publicPath: '/',
		filename: 'index.js',
		libraryTarget: 'umd',
		clean: true
	},
	devServer: {
		allowedHosts: 'all',
		compress: false,
		port: 8892,
		open: true,
		historyApiFallback: true,
		devMiddleware: {
			publicPath: '/',
			stats: {
				contentBase: path.join(__dirname, 'dist'),
				warnings: false
			}
		},
		client: {
			overlay: false
		}
	}
});
