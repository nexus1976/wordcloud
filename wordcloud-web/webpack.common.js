const path = require('path');

module.exports = {
	resolve: { 
		extensions: ['.ts', '.tsx', '.js', 'jsx'],
		fallback: {
			'fs': false,
			'os': false,
			'path': false
		}
	},
	module: {
		rules: [
			{
				test: /\.(ts|js)x?$/,
				include: [
					path.resolve(__dirname, 'src')
				],
				loader: 'babel-loader',
				options: {
					babelrc: false,
					presets: [
						'@babel/preset-env',
						'solid',
						'@babel/preset-typescript'
					]
				}
			},
			{
				test: /\.(png|jpg|gif|svg)$/,
				use: [
					{
						loader: 'url-loader',
						options: {
							limit: 12000, // if less than 12000 bytes, add base64 encoded
							// image to css
							name: (file) => `/[path][name].[ext]`,
						},
					},
				],
			},
			{
				test: /\.(woff(2)?|ttf|eot)(\?v=\d+\.\d+\.\d+)?$/,
				use: [
					{
						loader: 'file-loader',
						options: {
							name: '[name].[ext]',
							outputPath: 'fonts/',
						},
					},
				],
			},
			{
				test: /\.s[ac]ss$/i,
				use: ['style-loader', 'css-loader', 'sass-loader'],
			},
		]		
	}
};
